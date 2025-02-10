using Common.Models;
using Common.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySetItem
{
    public partial class Main : Form
    {
        public string _dfGearUrl = "https://api.dfgear.xyz";
        public string _dfDunDamUrl = "https://dundam.xyz";

        public List<string> SetItems = new List<string>()
        {
            "영원히 이어지는 황금향 세트",
            "칠흑의 정화 세트",
            "세렌디피티 세트",
            "한계를 넘어선 에너지 세트",
            "소울 페어리 세트",
            "압도적인 자연 세트",
            "고대 전장의 발키리 세트",
            "에테리얼 오브 아츠 세트",
            "그림자에 숨은 죽음 세트",
            "무리 사냥의 길잡이 세트",
            "마력의 영역 세트",
            "용투장의 난 세트"
        };

        public Main()
        {
            InitializeComponent();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("캐릭터 명 필요.");
                return;
            }

            btnSearch.Enabled = false;
            lblStat.Text = "조회중";

            string name = txtName.Text;
            string serverName = cbServer.Text;

            try
            {
                await RunAsync(name, serverName);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message + Environment.NewLine + "입력한 캐릭터가 던담, 던파기어에서 조회된 내역이 없거나 잘못된경우 입니다. 확인후 다시 해보세요.", "알람");
            }
            btnSearch.Enabled = true;
            lblStat.Text = "완료";
        }



        public async Task RunAsync(string userId, string serverName)
        {
            Common.Utils.DfGearHelper gear = new Common.Utils.DfGearHelper(_dfGearUrl);
            List<Common.Models.DfGear.ItemDetail> result = await gear.GetTimeLineItems(userId, serverName);

            if (result != null && result.Count > 0)
            {
                Console.WriteLine("===============");
                // SetItemName 기준으로 그룹화
                var groupedItems = result
                        .Where(x => string.IsNullOrEmpty(x.ConvertSetItem) == false)
                        .OrderBy(item => item.ItemType)
                        .ThenByDescending(item => item.ItemRarityLevel)
                        .ThenByDescending(item => item.Date)
                        .GroupBy(item => item.ConvertSetItem)
                        ;

                StringBuilder outputListSetItem = new StringBuilder();
                foreach (var group in groupedItems)
                {
                    outputListSetItem.AppendLine($"<h4>{group.Key}</h4>");
                    //<div class="col-11">
                    outputListSetItem.AppendLine($"<div class='col-8'>");
                    outputListSetItem.AppendLine($"<table class='table table-bordered'>");
                    outputListSetItem.AppendLine($"<tr><th>아이템</th><th>이름</th><th>등급</th><th>부위</th><th>채널</th><th>획득 일</th></tr>");
                    foreach (var item in group)
                    {
                        outputListSetItem.AppendLine($"<tr><td><img width='28px' height='28px' src='https://img-api.neople.co.kr/df/items/{item.ItemId}'></td><td>{item.ItemName}</td><td class='{CodeHelper.GetRarityColor(item.ItemRarity)}'>{item.ItemRarity}</td><td>{item.ItemType}</td><td>{item.Channel}</td><td>{item.Date}</td></tr>");
                    }
                    outputListSetItem.AppendLine($"</table></div><br/>");
                }

                // 레어리티 별 갯수
                var rarityGroup = result
                        .GroupBy(item => item.ItemRarity )
                        .Select(group => new
                        {
                            Rarity = group.Key,
                            Count = group.Count()
                        })
                        .ToList();

                string rarityX = JsonConvert.SerializeObject(rarityGroup.Select(x => x.Rarity));
                string rarityY = JsonConvert.SerializeObject(rarityGroup.Select(x => x.Count));

                // 채널별 갯수
                var channelGroup = result
                        .GroupBy(item => item.Channel)
                        .Select(group => new
                        {
                            Channel = group.Key,
                            Count = group.Count()
                        }).Take(10)
                        .ToList();

                string channelX = JsonConvert.SerializeObject(channelGroup.Select(x => x.Channel));
                string channelY = JsonConvert.SerializeObject(channelGroup.Select(x => x.Count));

                // 머리어깨, 상의, 하의, 벨트, 신발, 팔찌, 목걸이, 보조장비, 반지, 귀걸이, 마법석
                // ConvertSetItem & ItemType 기준으로 그룹화 후 SetPoint가 가장 높은 항목 선택
                var bestItems = result
                    .Where(item => string.IsNullOrEmpty(item.ConvertSetItem) == false)
                    .GroupBy(item => new { item.ConvertSetItem, item.ItemType })
                    .Select(group => group.OrderByDescending(item => item.SetPoint).First())
                    .OrderBy(x => x.ConvertSetItem)
                    .ToList();
                 

                // 고유 아이템
                List<Common.Models.DfGear.ItemDetail> commonItems = bestItems.Where(x => x.ConvertSetItem == "고유").ToList();

                List<AvailableSetItem> allAvailableSetItem = new List<AvailableSetItem>();
                foreach (string setName in SetItems)
                {
                    AvailableSetItem addItem = new AvailableSetItem() { SetItemName = setName };
                    foreach (Common.Models.DfGear.ItemDetail item in bestItems.Where(x => x.SetItemName == setName))
                    {
                        addItem.SettingPoint(item);
                    }
                    foreach (Common.Models.DfGear.ItemDetail item in commonItems)
                    {
                        addItem.SettingPoint(item);
                    }

                    allAvailableSetItem.Add(addItem);
                }

                // 세트포인트 가장 높은거 체크
                int maxSetPoint = allAvailableSetItem.Max(x => x.AllPoint);
                var topItems = allAvailableSetItem.Where(x => x.AllPoint == maxSetPoint);
                foreach (var item in topItems) { item.IsTop = true; }



                // 던담 정보 조회
                Common.Utils.DfDunDamHelper dundam = new Common.Utils.DfDunDamHelper(_dfDunDamUrl);
                Common.Models.DfDunDam.CharInfo charInfo = await dundam.GetCharInfoAsync(userId, serverName);
                Common.Models.DfDunDam.CharDetailInfo charDetailInfo = await dundam.GetCharDetailInfoAsync(charInfo.CharacterKey, charInfo.ServerId);
                Common.Models.CharSummary charSummary = new CharSummary(charInfo, charDetailInfo);

                // 던담에서 융합석 정보 가져오기
                var charFusionItem = charSummary.GetFusionItem();

                // 착용 가능 세트정보에 융합석 정보 넣기
                // 융합석은 변환가능하기 때문에 착용중인 융합석 기준으로 넘김
                foreach(var item in allAvailableSetItem)
                {
                    item.SetFusionItem(charFusionItem);
                }




                StringBuilder outputAvailableSetItem = new StringBuilder();

                foreach (var setItem in allAvailableSetItem)
                {
                    outputAvailableSetItem.AppendLine(setItem.OutputHtml());
                }


                string htmlDoc = File.ReadAllText("layout.txt");
                string outputHtml = htmlDoc.Replace("{{CharInfo}}", $"{userId} / {serverName}")
                        .Replace("{{ListSetItem}}", outputListSetItem.ToString())
                        .Replace("{{AvailableSetItem}}", outputAvailableSetItem.ToString())
                        .Replace("{{CharKey}}", charSummary.GetCharacterKey())
                        .Replace("{{ServerId}}", charSummary.GetServerId())
                        .Replace("{{CharSummary}}", charSummary.GetCharSummaryHtml())
                        .Replace("{{UseItemSummary}}", charSummary.GetUseItemSummaryHtml())
                        .Replace("{{UseItemTitleSummary}}", charSummary.GetUseItemTitleSummaryHtml())
                        .Replace("{{RarityX}}", rarityX)
                        .Replace("{{RarityY}}", rarityY)
                        .Replace("{{ChannelX}}", channelX)
                        .Replace("{{ChannelY}}", channelY)
                        ;

                string fileName = $".\\output\\{DateTime.Now.ToString("yyyyMMddHHmmss")}_{Regex.Replace(userId, "[^가-힣a-zA-Z0-9 ]", "")}.html";

                File.WriteAllText(fileName, outputHtml);

                // 기본 브라우저에서 HTML 파일 열기
                Process.Start(new ProcessStartInfo
                {
                    FileName = fileName,
                    UseShellExecute = true  // 기본 프로그램(웹 브라우저)으로 실행
                });
            }
            else
            {
                //throw new Exception("입력한 캐릭터가 던담, 던파기어에서 조회된 내역이 없거나 잘못된경우 입니다. 확인후 다시 해보세요.");
            }
        }
    }
}
