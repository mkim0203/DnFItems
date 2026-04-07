using Common.Models;
using Common.Services;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPledgeSetItem
{
    public partial class Main : Form
    {
        public string _dfMaxUrl = "https://dfmax.xyz";
        public string _dfApiUrl = "https://api.neople.co.kr";

        public Main()
        {
            InitializeComponent();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("모험단 명 필요.");
                return;
            }

            btnSearch.Enabled = false;
            lblStat.Text = "조회중";

            string advenName = txtName.Text;

            try
            {
                await RunAsync(advenName);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + "조회중 오류가 발생했습니다. 입력정보를 다시 확인후 재시도 해보세요.", "알람");
            }
            btnSearch.Enabled = true;

        }


        public async Task RunAsync(string advenName)
        {
            //List<TimeLineItem> itemTimeLine = await DnfApiService.GetAllTimeLineAsync(userId, serverName);

            //if (itemTimeLine == null || itemTimeLine.Count == 0)
            //{
            //    lblStat.Text = "캐릭터 정보가 조회되지 않았습니다.";
            //    return;
            //}

            // 던파 max 모헌단 조회
            Common.Utils.DfMaxHelper dfmax = new Common.Utils.DfMaxHelper(_dfMaxUrl);
            var charInfos = await dfmax.GetAdvenUsersAsync(advenName);

            if (charInfos == null || charInfos.Count == 0)
            {
                return;
            }

            StringBuilder outputCharTab = new StringBuilder();
            StringBuilder outputCharTabContent = new StringBuilder();


            int index = 0;
            foreach (var maxCharInfo in charInfos)
            {
                lblStat.Text = $"{++index} / {charInfos.Count} 조회중";


                StringBuilder outputListSetItem = new StringBuilder();
                StringBuilder outputPledgeData = new StringBuilder();
                StringBuilder outputUsedPledgeItem = new StringBuilder();

                string userId = maxCharInfo.Name;
                string serverId = maxCharInfo.ServerId;

                // 던파 api 조회
                Common.Utils.DnfApiHelper dnfApi = new Common.Utils.DnfApiHelper(_dfApiUrl);
                Common.Models.DnfApi.CharInfo charInfo = await dnfApi.GetCharInfoAsync(userId, serverId, true);
                Common.Models.DnfApi.EquipmentResult equipment = await dnfApi.GetEquipmentsAsync(charInfo.CharacterId, serverId, true);

                Common.Models.DnfApi.OathInfoResult oathInfo =  await dnfApi.GetCharOathAsync(charInfo.CharacterId, serverId, true);

                Common.Models.DnfApiCharSummary charSummary = new Common.Models.DnfApiCharSummary(charInfo, equipment);

                List<TimeLinePledgeItem> pledgeTimeLine = await DnfApiService.GetAllPledgeTimeLineAsync(userId, serverId, true);


                

                PledgeSummary pledgeSummary = new PledgeSummary(pledgeTimeLine);

                foreach (var item in pledgeSummary.SummaryInfo)
                {
                    // 사용중인 세트인가?
                    bool isUseSet = false;
                    if(oathInfo?.Oath?.SetInfo?.SetName != null)
                    {
                        if(oathInfo?.Oath?.SetInfo?.SetName.IndexOf(item.Name) > -1)
                        {
                            isUseSet = true;
                        }
                    }
                    
                    //Console.WriteLine($"{item.Name} / {item.PledgeRarity} / {item.BegCount} / {item.EpiCount} / {item.LegCount} / {item.Session10LegCount}");
                    outputPledgeData.AppendLine($"<tr>");
                    outputPledgeData.AppendLine($"<td>{item.Name}{(isUseSet ? "*" : string.Empty)}</td><td>{item.PledgeRarity}</td><td>{item.BegCount}</td><td>{item.EpiCount}</td><td>{item.LegCount}</td><td>{item.Session10LegCount}</td>");
                    outputPledgeData.AppendLine($"</tr>");
                }

                // 서약 정보에서 가장높은 레어리티 level 조회. 레전더리 이상
                var findTarget = pledgeSummary.SummaryInfo.Where(x => x.PledgeRarityLevel >= 2).OrderByDescending(x => x.PledgeRarityLevel).FirstOrDefault();
                int maxPledgeLevel = 2; // 기본값 레전더리
                if (findTarget != null) maxPledgeLevel = findTarget.PledgeRarityLevel;

                // 서약정보에 가장 높은 레어리티 level과 일치하는 세트명 조회
                List<string> temp = pledgeSummary.SummaryInfo.Where(x => x.PledgeRarityLevel == maxPledgeLevel && x.PledgeRarityLevel >= 2).Select(x => x.Name).ToList();
                string maxRarityPledges = string.Join(", ", temp);

                // 캐릭터 탭 생성
                outputCharTab.AppendLine($@"<li class=""nav-item"" role=""presentation"">
            <button class=""nav-link
                {(index <= 1 ? " active" : "")}"" id=""{maxCharInfo.CharacterKey}"" data-bs-toggle=""tab"" data-bs-target=""#{maxCharInfo.CharacterKey}-tab-pane"" type=""button"" role=""tab"" aria-controls=""{maxCharInfo.CharacterKey}-tab-pane"" aria-selected=""{(index <= 1 ? "true" : "fasle")}""><div>{maxCharInfo.Name}</div>
<div>{charSummary?.GetSetName() ?? string.Empty} ( {charSummary?.GetSetGrade() ?? string.Empty} )</div>
<div class=""{CodeHelper.GetRarityColor(oathInfo?.Oath?.SetInfo?.SetRarityName ?? string.Empty)}"">{oathInfo?.Oath?.SetInfo?.SetName ?? string.Empty} ( {oathInfo?.Oath?.SetInfo?.SetRarityName ?? string.Empty} )</div>
<div class=""{CodeHelper.GetRarityColor(findTarget?.PledgeRarity ?? string.Empty)}"">서약 : {maxRarityPledges}</div>
</button>
            </li>
                ");

                // SetItemName 기준으로 그룹화
                var groupedItems = pledgeTimeLine
                        .Where(x => string.IsNullOrEmpty(x.SetItemName) == false)
                        //.OrderBy(item => item.SlotOrder)
                        .OrderByDescending(x => x.ItemRarityLevel)
                        .ThenBy(x => x.ItemName)
                        .ThenByDescending(x => x.Date)
                        .GroupBy(x => x.SetItemName);



                foreach (var group in groupedItems)
                {
                    outputListSetItem.AppendLine($"<h4>{group.Key}</h4>");
                    //<div class="col-11">
                    outputListSetItem.AppendLine($"<div class='col-11'>");
                    outputListSetItem.AppendLine($"<table class='table table-bordered'>");
                    outputListSetItem.AppendLine($@"<tr>
    <th style='width:40px'>item</th>
    <th>이름</th>
    <th style='width:100px'>등급</th>
    <th style='width:190px'>채널</th>
    <th style='width:190px'>던전</th>
    <th style='width:220px'>획득 방법</th>
    <th style='width:150px'>획득 일</th>
</tr>");
                    foreach (var grItem in group)
                    {
                        outputListSetItem.AppendLine($"<tr><td><img width='28px' height='28px' src='https://img-api.neople.co.kr/df/items/{grItem.ItemId}'></td><td>{grItem.ItemName}</td><td class='{CodeHelper.GetRarityColor(grItem.ItemRarity)}'>{grItem.ItemRarity}</td><td>{grItem.GetChannelInfo}</td><td>{grItem.DungeonName}</td><td>{grItem.Name}</td><td>{grItem.Date}</td></tr>");
                    }
                    outputListSetItem.AppendLine($"</table></div><br/>");
                }

                if (oathInfo?.Oath != null)
                {
                    outputUsedPledgeItem.Append($"<img width='28px' height='28px' src='https://img-api.neople.co.kr/df/items/{oathInfo?.Oath?.Info?.ItemId}'>");
                    foreach (var crystal in oathInfo?.Oath?.Crystal)
                    {
                        outputUsedPledgeItem.Append($"<img width='28px' height='28px' src='https://img-api.neople.co.kr/df/items/{crystal.ItemId}'>");
                    }
                    // <img width='28px' height='28px' src='https://img-api.neople.co.kr/df/items/{grItem.ItemId}'>
                }

                outputCharTabContent.AppendLine($@"<div class=""tab-pane fade{(index == 1 ? " show active" : "")}"" id=""{maxCharInfo.CharacterKey}-tab-pane"" role=""tabpanel"" aria-labelledby=""{maxCharInfo.CharacterKey}-tab"" tabindex=""0"">
<div>");
                string subHtmlDoc = File.ReadAllText("Pledgelayout_Sub1.txt");
                outputCharTabContent.AppendLine(subHtmlDoc.Replace("{{CharInfo}}", $"{userId} / {serverId}")
                        .Replace("{{ListSetItem}}", outputListSetItem.ToString())
                        .Replace("{{CharKey}}", charSummary.GetCharacterKey())
                        .Replace("{{ServerId}}", charSummary.GetServerId())
                        .Replace("{{CharSummary}}", charSummary.GetCharSummaryHtml())
                        .Replace("{{UseItemSummary}}", charSummary.GetUseItemSummaryHtml())
                        .Replace("{{UseItemTitleSummary}}", charSummary.GetUseItemTitleSummaryHtml())
                        .Replace("{{UseOathTitleSummary}}", $"{oathInfo?.Oath?.SetInfo?.SetPoint ?? 0} - {oathInfo?.Oath?.SetInfo?.SetName ?? string.Empty} ( {oathInfo?.Oath?.SetInfo?.SetRarityName ?? string.Empty} )")
                        .Replace("{{UsePledgeItemSummary}}", outputUsedPledgeItem.ToString())
                        .Replace("{{PledgeSummary}}", outputPledgeData.ToString())

                );
                outputCharTabContent.AppendLine("</div></div>");
            }


            string htmlDoc = File.ReadAllText("Pledgelayout.txt");
            string outputHtml = htmlDoc
                    //.Replace("{{CharInfo}}", $"{userId} / {serverId}")
                    .Replace("{{AdvenName}}", advenName)
                    .Replace("{{SearchTime}}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    .Replace("{{ListCharTab}}", outputCharTab.ToString())
                    .Replace("{{ListCharTabContent}}", outputCharTabContent.ToString())

                    ;

            string fileName = $".\\output\\{DateTime.Now.ToString("yyyyMMddHHmmss")}_{Regex.Replace(advenName, "[^가-힣a-zA-Z0-9 ]", "")}.html";

            File.WriteAllText(fileName, outputHtml);

            // 기본 브라우저에서 HTML 파일 열기
            Process.Start(new ProcessStartInfo
            {
                FileName = fileName,
                UseShellExecute = true  // 기본 프로그램(웹 브라우저)으로 실행
            });

            lblStat.Text = "완료.";

        }
        private async void Main_Load(object sender, EventArgs e)
        {


            this.Text += $"({Application.ProductVersion})";

            if (await CheckNewVersion())
            {
                lblStat.Text += Environment.NewLine + "새로운 버전이 있습니다. 업데이트를 확인해주세요.";
            }
        }

        private async Task<bool> CheckNewVersion()
        {
            return false;
            //try
            //{
            //    GithubHelper helper = new GithubHelper("https://raw.githubusercontent.com/");
            //    var version = await helper.GetLastVersionAsync();
            //    Console.WriteLine(version);

            //    Version currentVersion = new Version(Application.ProductVersion);

            //    var newVersion = new Version(version);
            //    if (currentVersion.CompareTo(newVersion) < 0)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //catch { return false; }
        }
    }

}