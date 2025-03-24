using Common.Models;
using Common.Services;
using Common.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MySetItem
{
    public partial class Main : Form
    {
        public string _dfMaxUrl = "https://dfmax.xyz";
        public string _dfApiUrl = "https://api.neople.co.kr";

        private List<string> _serverList { get; set; }
        public List<string> ServerList
        { 
            get
            {
                if (_serverList == null || _serverList.Count == 0)
                {
                    _serverList = new List<string>()
                    {
                        "카인",
                        "디레지에",
                        "시로코",
                        "프레이",
                        "카시야스",
                        "힐더",
                        "안톤",
                        "바칼"
                    };

                    try
                    {
                        string option = ConfigurationManager.AppSettings["ServersOption"];
                        if (option == "1")
                        {
                            _serverList.Add("길드");
                            _serverList.Insert(0, "모험단");
                        }
                    }
                    catch
                    {

                    }

                }
                return _serverList;
            }
        }

        public Main()
        {
            InitializeComponent();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(cbServer.Text))
            {
                MessageBox.Show("캐릭터 명 / 서버 명 필요.");
                return;
            }

            btnSearch.Enabled = false;
            lblStat.Text = "조회중";

            string name = txtName.Text;
            string serverName = cbServer.Text;

            try
            {
                if (serverName.Equals("모험단"))
                {
                    await RunAdvenAsync(name);
                }
                else if(serverName.Equals("길드"))
                {
                    await RunGuildAsync(name);
                }
                else
                {
                    await RunAsync(name, serverName);
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message + Environment.NewLine + "조회중 오류가 발생했습니다. 입력정보를 다시 확인후 재시도 해보세요.", "알람");
            }
            btnSearch.Enabled = true;
            
        }



        public async Task RunAsync(string userId, string serverName)
        {
            List<TimeLineItem> itemTimeLine = await DnfApiService.GetAllTimeLineAsync(userId, serverName);
            
            if (itemTimeLine == null || itemTimeLine.Count == 0)
            {
                lblStat.Text = "캐릭터 정보가 조회되지 않았습니다.";
                return;
            }

            List<TimeLineRegion> regionTimeLine = await DnfApiService.GetAllTimeLineRegionAsync(userId, serverName);

            var allBeg = itemTimeLine.Where(x => x.ItemRarity.Equals("태초"));
            var allEpi = itemTimeLine.Where(x => x.ItemRarity.Equals("에픽"));
            var allLeg = itemTimeLine.Where(x => x.ItemRarity.Equals("레전더리"));

            var allBaseHell = itemTimeLine.Where(x => x.IsBaseHell);
            var allBaseHellBeg = allBaseHell.Where(x => x.ItemRarity.Equals("태초"));
            var allBaseHellEpi = allBaseHell.Where(x => x.ItemRarity.Equals("에픽"));
            var allBaseHellLeg = allBaseHell.Where(x => x.ItemRarity.Equals("레전더리"));

            var allSpHell = itemTimeLine.Where(x => x.IsSpHell);
            var allSpHellBeg = allSpHell.Where(x => x.ItemRarity.Equals("태초"));
            var allSphellEpi = allSpHell.Where(x => x.ItemRarity.Equals("에픽"));
            var allSphellLeg = allSpHell.Where(x => x.ItemRarity.Equals("레전더리"));

            var allWeekly = itemTimeLine.Where(x => x.IsWeekly);
            var allWeeklyBeg = allWeekly.Where(x => x.ItemRarity.Equals("태초"));
            var allWeeklyEpi = allWeekly.Where(x => x.ItemRarity.Equals("에픽"));
            var allWeeklyLeg = allWeekly.Where(x => x.ItemRarity.Equals("레전더리"));

            var allRegion = itemTimeLine.Where(x => x.IsRegion);
            var allRegionBeg = allRegion.Where(x => x.ItemRarity.Equals("태초"));
            var allRegionEpi = allRegion.Where(x => x.ItemRarity.Equals("에픽"));
            var allRegionLeg = allRegion.Where(x => x.ItemRarity.Equals("레전더리"));

            var allDaily = itemTimeLine.Where(x => x.IsDaily);
            var allDailyBeg = allDaily.Where(x => x.ItemRarity.Equals("태초"));
            var allDailyEpi = allDaily.Where(x => x.ItemRarity.Equals("에픽"));
            var allDailyLeg = allDaily.Where(x => x.ItemRarity.Equals("레전더리"));

            var allBaseDungeon = itemTimeLine.Where(x => x.IsBaseDungeon);
            var allBaseDungeonBeg = allBaseDungeon.Where(x => x.ItemRarity.Equals("태초"));
            var allBaseDungeonEpi = allBaseDungeon.Where(x => x.ItemRarity.Equals("에픽"));
            var allBaseDungeonLeg = allBaseDungeon.Where(x => x.ItemRarity.Equals("레전더리"));


            Console.WriteLine("===============");
            // SetItemName 기준으로 그룹화
            var groupedItems = itemTimeLine
                    .Where(x => string.IsNullOrEmpty(x.ConvertSetItem) == false)
                    .OrderBy(item => item.SlotOrder)
                    .ThenByDescending(item => item.ItemRarityLevel)
                    .ThenBy(item => item.ItemName)
                    .ThenByDescending(item => item.Date)
                    .GroupBy(item => item.ConvertSetItem);

            StringBuilder outputListSetItem = new StringBuilder();
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
    <th style='width:100px'>부위</th>
    <th style='width:190px'>채널</th>
    <th style='width:190px'>던전</th>
    <th style='width:220px'>획득 방법</th>
    <th style='width:150px'>획득 일</th>
</tr>");
                foreach (var item in group)
                {
                    outputListSetItem.AppendLine($"<tr><td><img width='28px' height='28px' src='https://img-api.neople.co.kr/df/items/{item.ItemId}'></td><td>{item.ItemName}</td><td class='{CodeHelper.GetRarityColor(item.ItemRarity)}'>{item.ItemRarity}</td><td>{item.Slot}</td><td>{item.GetChannelInfo}</td><td>{item.DungeonName}</td><td>{item.Name}</td><td>{item.Date}</td></tr>");
                }
                outputListSetItem.AppendLine($"</table></div><br/>");
            }

            #region 차트 통계 정보
            // 레어리티 별 갯수
            var rarityGroup = itemTimeLine
                    .GroupBy(item => item.ItemRarity)
                    .Select(group => new
                    {
                        Rarity = group.Key,
                        Count = group.Count()
                    })
                    .ToList();

            string rarityX = JsonConvert.SerializeObject(rarityGroup.Select(x => x.Rarity));
            string rarityY = JsonConvert.SerializeObject(rarityGroup.Select(x => x.Count));

            // 채널별 갯수
            var channelGroup = itemTimeLine
                    .GroupBy(item => item.GetChannelInfo)
                    .Select(group => new
                    {
                        Channel = group.Key,
                        Count = group.Count()
                    }).Take(10)
                    .ToList();

            string channelX = JsonConvert.SerializeObject(channelGroup.Select(x => x.Channel));
            string channelY = JsonConvert.SerializeObject(channelGroup.Select(x => x.Count));

            var hellGroup = itemTimeLine
                    .Where(x => x.IsBaseHell)
                    .GroupBy(item => item.ItemRarity)
                    .Select(group => new
                    {
                        Rarity = group.Key,
                        Count = group.Count()
                    }).Take(10)
                    .ToList();

            string hellX = JsonConvert.SerializeObject(hellGroup.Select(x => x.Rarity));
            string hellY = JsonConvert.SerializeObject(hellGroup.Select(x => x.Count));

            var spHellGroup = itemTimeLine
                   .Where(x => x.IsSpHell)
                   .GroupBy(item => item.ItemRarity)
                   .Select(group => new
                   {
                       Rarity = group.Key,
                       Count = group.Count()
                   }).Take(10)
                   .ToList();

            string spHellX = JsonConvert.SerializeObject(spHellGroup.Select(x => x.Rarity));
            string spHellY = JsonConvert.SerializeObject(spHellGroup.Select(x => x.Count));

            var weeklyGroup = itemTimeLine
                   .Where(x => x.IsWeekly)
                   .GroupBy(item => item.DungeonName)
                   .Select(group => new
                   {
                       DungeonName = group.Key,
                       Count = group.Count()
                   }).Take(10)
                   .ToList();

            string weeklyX = JsonConvert.SerializeObject(weeklyGroup.Select(x => x.DungeonName));
            string weeklyY = JsonConvert.SerializeObject(weeklyGroup.Select(x => x.Count));

            // 머리어깨, 상의, 하의, 벨트, 신발, 팔찌, 목걸이, 보조장비, 반지, 귀걸이, 마법석
            // ConvertSetItem & Slot 기준으로 그룹화 후 SetPoint가 가장 높은 항목 선택
            var bestItems = itemTimeLine
                .Where(item => string.IsNullOrEmpty(item.ConvertSetItem) == false)
                .GroupBy(item => new { item.ConvertSetItem, item.Slot })
                .Select(group => group.OrderByDescending(item => item.SetPoint).First())
                .OrderBy(x => x.ConvertSetItem)
                .ToList();

            #endregion

            StringBuilder outputListCountSummary = new StringBuilder();

            if (itemTimeLine.Count > 0)
            {
                outputListCountSummary.AppendLine($"<h4>획득 현황</h4>");
                //<div class="col-11">
                outputListCountSummary.AppendLine($"<div class='col-6'>");
                outputListCountSummary.AppendLine($"<table class='table table-bordered'>");
                outputListCountSummary.AppendLine($@"<tr>
    <th>구분</th>
    <th style='width:100px'>태초</th>
    <th style='width:100px'>에픽</th>
    <th style='width:100px'>레전</th>
    <th style='width:100px'>합계</th>
</tr>");

                // 전체, 헬, 심연, 상던, 레기온, 환요

                outputListCountSummary.AppendLine($"<tr><td>전체</td><td>{allBeg.Count()}</td><td>{allEpi.Count()}</td><td>{allLeg.Count()}</td><td>{itemTimeLine.Count()}</td></tr>");
                
                outputListCountSummary.AppendLine($"<tr><td>심연 : 종말</td><td>{allSpHellBeg.Count()}</td><td>{allSphellEpi.Count()}</td><td>{allSphellLeg.Count()}</td><td>{allSpHell.Count()}</td></tr>");
                outputListCountSummary.AppendLine($"<tr><td>종말</td><td>{allBaseHellBeg.Count()}</td><td>{allBaseHellEpi.Count()}</td><td>{allBaseHellLeg.Count()}</td><td>{allBaseHell.Count()}</td></tr>");
                outputListCountSummary.AppendLine($"<tr><td>상던</td><td>{allWeeklyBeg.Count()}</td><td>{allWeeklyEpi.Count()}</td><td>{allWeeklyLeg.Count()}</td><td>{allWeekly.Count()}</td></tr>");
                outputListCountSummary.AppendLine($"<tr><td>레기온</td><td>{allRegionBeg.Count()}</td><td>{allRegionEpi.Count()}</td><td>{allRegionLeg.Count()}</td><td>{allRegion.Count()}</td></tr>");
                outputListCountSummary.AppendLine($"<tr><td>환요</td><td>{allDailyBeg.Count()}</td><td>{allDailyEpi.Count()}</td><td>{allDailyLeg.Count()}</td><td>{allDaily.Count()}</td></tr>");
                outputListCountSummary.AppendLine($"<tr><td>기본던전</td><td>{allBaseDungeonBeg.Count()}</td><td>{allBaseDungeonEpi.Count()}</td><td>{allBaseDungeonLeg.Count()}</td><td>{allBaseDungeon.Count()}</td></tr>");

                outputListCountSummary.AppendLine($"</table></div><br/>");
            }

            #region 세트별 가능 등급
            List<Common.Models.TimeLineItem> commonItems = bestItems.Where(x => x.ConvertSetItem == "고유").ToList();

            List<AvailableSetItem> allAvailableSetItem = new List<AvailableSetItem>();
            foreach (string setName in CodeHelper.SetItems)
            {
                AvailableSetItem addItem = new AvailableSetItem() { SetItemName = setName };
                foreach (Common.Models.TimeLineItem item in bestItems.Where(x => x.SetItemName == setName))
                {
                    addItem.SettingPoint(item);
                }
                foreach (Common.Models.TimeLineItem item in commonItems)
                {
                    addItem.SettingPoint(item);
                }

                allAvailableSetItem.Add(addItem);
            }

            // 세트포인트 가장 높은거 체크
            int maxSetPoint = allAvailableSetItem.Max(x => x.AllPoint);
            var topItems = allAvailableSetItem.Where(x => x.AllPoint == maxSetPoint);
            foreach (var item in topItems) { item.IsTop = true; }

            // 던파 api 조회
            Common.Utils.DnfApiHelper dnfApi = new Common.Utils.DnfApiHelper(_dfApiUrl);
            Common.Models.DnfApi.CharInfo charInfo = await dnfApi.GetCharInfoAsync(userId, serverName);
            Common.Models.DnfApi.EquipmentResult equipment = await dnfApi.GetEquipmentsAsync(charInfo.CharacterId, serverName);
            Common.Models.DnfApiCharSummary charSummary = new Common.Models.DnfApiCharSummary(charInfo, equipment);

            // 융합석 정보 가져오기
            var charFusionItem = charSummary.GetFusionItem();

            // 착용 가능 세트정보에 융합석 정보 넣기
            // 융합석은 변환가능하기 때문에 착용중인 융합석 기준으로 넘김
            foreach (var item in allAvailableSetItem)
            {
                item.SetFusionItem(charFusionItem);
            }


            StringBuilder outputAvailableSetItem = new StringBuilder();

            foreach (var setItem in allAvailableSetItem.OrderByDescending(x => x.AllPoint))
            {
                outputAvailableSetItem.AppendLine(setItem.OutputHtml());
            } 
            #endregion

            #region 레기온
            StringBuilder outputRegionItem = new StringBuilder();

            if(regionTimeLine.Count > 0)
            {
                outputRegionItem.AppendLine($"<h3 class='mt-5 text-center'>획득 : {allRegionBeg.Count()} / {allRegionEpi.Count()} / {allRegionLeg.Count()}</h3>");
            }
            // 레기온 정리
            foreach (var region in regionTimeLine.OrderByDescending(x => x.Date))
            {
                outputRegionItem.AppendLine($"<h4>클리어 : {region.Date}</h4>");
                //<div class="col-11">
                outputRegionItem.AppendLine($"<div class='col-6'>");
                outputRegionItem.AppendLine($"<table class='table table-bordered'>");
                outputRegionItem.AppendLine($@"<tr>
    <th style='width:40px'>item</th>
    <th>이름</th>
    <th style='width:100px'>등급</th>
    <th style='width:100px'>부위</th>
</tr>");
                foreach (var item in allRegion.Where(x => x.Date == region.Date))
                {
                    outputRegionItem.AppendLine($"<tr><td><img width='28px' height='28px' src='https://img-api.neople.co.kr/df/items/{item.ItemId}'></td><td>{item.ItemName}</td><td class='{CodeHelper.GetRarityColor(item.ItemRarity)}'>{item.ItemRarity}</td><td>{item.Slot}</td></tr>");
                }
                outputRegionItem.AppendLine($"</table></div><br/>");
            }
            #endregion

            #region 심연
            // 심연 목록
            var spHellGroup2 = allSpHell
                    .OrderByDescending(x => x.Date)
                    .GroupBy(item => item.Date)
                    ;
            StringBuilder outputSpHellItem = new StringBuilder();

            if (spHellGroup2.Count() > 0)
            {
                outputSpHellItem.AppendLine($"<h3 class='mt-5 text-center'> 총 횟수 : {spHellGroup2.Count()}, 획득 : {allSpHellBeg.Count()} / {allSphellEpi.Count()} / {allSphellLeg.Count()}</h3>");
            }

            foreach (var spHell in spHellGroup2)
            {
                outputSpHellItem.AppendLine($"<h4>{spHell.Key}</h4>");
                //<div class="col-11">
                outputSpHellItem.AppendLine($"<div class='col-6'>");
                outputSpHellItem.AppendLine($"<table class='table table-bordered'>");
                outputSpHellItem.AppendLine($@"<tr>
    <th style='width:40px'>item</th>
    <th>이름</th>
    <th style='width:100px'>등급</th>
    <th style='width:100px'>부위</th>
</tr>");
                foreach (var item in spHell)
                {
                    outputSpHellItem.AppendLine($"<tr><td><img width='28px' height='28px' src='https://img-api.neople.co.kr/df/items/{item.ItemId}'></td><td>{item.ItemName}</td><td class='{CodeHelper.GetRarityColor(item.ItemRarity)}'>{item.ItemRarity}</td><td>{item.Slot}</td></tr>");
                }
                outputSpHellItem.AppendLine($"</table></div><br/>");
            }
            #endregion

            #region 헬
            // 헬 목록
            var baseHellGroup = allBaseHell
                    .OrderByDescending(x => x.Date)
                    .GroupBy(item => item.DateDay)
                    ;
            StringBuilder outputBaseHellItem = new StringBuilder();

            if (baseHellGroup.Count() > 0)
            {
                outputBaseHellItem.AppendLine($"<h3 class='mt-5 text-center'> 획득 : {allBaseHellBeg.Count()} / {allBaseHellEpi.Count()} / {allBaseHellLeg.Count()}</h3>");
            }

            foreach (var baseHell in baseHellGroup)
            {
                outputBaseHellItem.AppendLine($"<h4>{baseHell.Key}</h4>");
                //<div class="col-11">
                outputBaseHellItem.AppendLine($"<div class='col-11'>");
                outputBaseHellItem.AppendLine($"<table class='table table-bordered'>");
                outputBaseHellItem.AppendLine($@"<tr>
    <th style='width:40px'>item</th>
    <th>이름</th>
    <th style='width:100px'>등급</th>
    <th style='width:100px'>부위</th>
    <th style='width:190px'>채널</th>
    <th style='width:190px'>던전</th>
    <th style='width:220px'>획득 방법</th>
    <th style='width:150px'>획득 일</th>
</tr>");
                foreach (var item in baseHell)
                {
                    outputBaseHellItem.AppendLine($"<tr><td><img width='28px' height='28px' src='https://img-api.neople.co.kr/df/items/{item.ItemId}'></td><td>{item.ItemName}</td><td class='{CodeHelper.GetRarityColor(item.ItemRarity)}'>{item.ItemRarity}</td><td>{item.Slot}</td><td>{item.GetChannelInfo}</td><td>{item.DungeonName}</td><td>{item.Name}</td><td>{item.Date}</td></tr>");
                }
                outputBaseHellItem.AppendLine($"</table></div><br/>");
            }
            #endregion

            #region 상급던전
            var weeklyGroup2 = allWeekly
                    .OrderByDescending(x => x.DateDay)
                    .GroupBy(item => item.DateDay)
                    ;
            StringBuilder outputWeeklyItem = new StringBuilder();

            if (weeklyGroup2.Count() > 0)
            {
                outputWeeklyItem.AppendLine($"<div class='col-6'>");
                outputWeeklyItem.AppendLine($"<table class='table table-bordered'>");
                outputWeeklyItem.AppendLine($@"<tr>
    <th>구분</th>
    <th style='width:100px'>태초</th>
    <th style='width:100px'>에픽</th>
    <th style='width:100px'>레전</th>
    <th style='width:100px'>합계</th>
</tr>");

                outputWeeklyItem.AppendLine($@"<tr><td>전체</td>
    <td>{allWeeklyBeg.Count()}</td>
    <td>{allWeeklyEpi.Count()}</td>
    <td>{allWeeklyLeg.Count()}</td>
    <td>{allWeekly.Count()}</td>
</tr>");

                foreach (string dungeonName in CodeHelper.WeeklyDungeonNames)
                {
                    outputWeeklyItem.Append($@"<tr><td>{dungeonName}</td>
    <td>{allWeeklyBeg.Where(x => x.DungeonName.Equals(dungeonName)).Count()}</td>
    <td>{allWeeklyEpi.Where(x => x.DungeonName.Equals(dungeonName)).Count()}</td>
    <td>{allWeeklyLeg.Where(x => x.DungeonName.Equals(dungeonName)).Count()}</td>
    <td>{allWeekly.Where(x => x.DungeonName.Equals(dungeonName)).Count()}</td>
</tr>");
                }

                outputWeeklyItem.AppendLine($"</table></div><br/>");
            }

            foreach (var weekly in weeklyGroup2)
            {
                outputWeeklyItem.AppendLine($"<h4>{weekly.Key}</h4>");
                //<div class="col-11">
                outputWeeklyItem.AppendLine($"<div class='col-11'>");
                outputWeeklyItem.AppendLine($"<table class='table table-bordered'>");
                outputWeeklyItem.AppendLine($@"<tr>
    <th style='width:40px'>item</th>
    <th>이름</th>
    <th style='width:100px'>등급</th>
    <th style='width:100px'>부위</th>
    <th style='width:190px'>채널</th>
    <th style='width:190px'>던전</th>
    <th style='width:220px'>획득 방법</th>
    <th style='width:150px'>획득 일</th>
</tr>");
                foreach (var item in weekly)
                {
                    outputWeeklyItem.AppendLine($"<tr><td><img width='28px' height='28px' src='https://img-api.neople.co.kr/df/items/{item.ItemId}'></td><td>{item.ItemName}</td><td class='{CodeHelper.GetRarityColor(item.ItemRarity)}'>{item.ItemRarity}</td><td>{item.Slot}</td><td>{item.GetChannelInfo}</td><td>{item.DungeonName}</td><td>{item.Name}</td><td>{item.Date}</td></tr>");
                }
                outputWeeklyItem.AppendLine($"</table></div><br/>");
            }
            #endregion

            #region 환요
            var dailyGroup = allDaily
                    .OrderByDescending(x => x.Date)
                    .GroupBy(item => item.DateMonth)
                    ;
            StringBuilder outputDailyItem = new StringBuilder();

            if (dailyGroup.Count() > 0)
            {
                outputDailyItem.AppendLine($"<div class='col-6'>");
                outputDailyItem.AppendLine($"<table class='table table-bordered'>");
                outputDailyItem.AppendLine($@"<tr>
    <th>구분</th>
    <th style='width:100px'>태초</th>
    <th style='width:100px'>에픽</th>
    <th style='width:100px'>레전</th>
    <th style='width:100px'>합계</th>
</tr>");

                outputDailyItem.AppendLine($@"<tr><td>전체</td>
    <td>{allDailyBeg.Count()}</td>
    <td>{allDailyEpi.Count()}</td>
    <td>{allDailyLeg.Count()}</td>
    <td>{allDaily.Count()}</td>
</tr>");

                foreach (string dungeonName in CodeHelper.DailyDungeonNames)
                {
                    outputDailyItem.Append($@"<tr><td>{dungeonName}</td>
    <td>{allDailyBeg.Where(x => x.DungeonName.Equals(dungeonName)).Count()}</td>
    <td>{allDailyEpi.Where(x => x.DungeonName.Equals(dungeonName)).Count()}</td>
    <td>{allDailyLeg.Where(x => x.DungeonName.Equals(dungeonName)).Count()}</td>
    <td>{allDaily.Where(x => x.DungeonName.Equals(dungeonName)).Count()}</td>
</tr>");
                }

                outputDailyItem.AppendLine($"</table></div><br/>");
            }

            foreach (var daily in dailyGroup)
            {
                outputDailyItem.AppendLine($"<h4>{daily.Key}</h4>");
                //<div class="col-11">
                outputDailyItem.AppendLine($"<div class='col-11'>");
                outputDailyItem.AppendLine($"<table class='table table-bordered'>");
                outputDailyItem.AppendLine($@"<tr>
    <th style='width:40px'>item</th>
    <th>이름</th>
    <th style='width:100px'>등급</th>
    <th style='width:100px'>부위</th>
    <th style='width:190px'>채널</th>
    <th style='width:190px'>던전</th>
    <th style='width:220px'>획득 방법</th>
    <th style='width:150px'>획득 일</th>
</tr>");
                foreach (var item in daily)
                {
                    outputDailyItem.AppendLine($"<tr><td><img width='28px' height='28px' src='https://img-api.neople.co.kr/df/items/{item.ItemId}'></td><td>{item.ItemName}</td><td class='{CodeHelper.GetRarityColor(item.ItemRarity)}'>{item.ItemRarity}</td><td>{item.Slot}</td><td>{item.GetChannelInfo}</td><td>{item.DungeonName}</td><td>{item.Name}</td><td>{item.Date}</td></tr>");
                }
                outputDailyItem.AppendLine($"</table></div><br/>");
            }
            #endregion


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
                    .Replace("{{HellX}}", hellX)
                    .Replace("{{HellY}}", hellY)
                    .Replace("{{SpHellX}}", spHellX)
                    .Replace("{{SpHellY}}", spHellY)
                    .Replace("{{WeeklyX}}", weeklyX)
                    .Replace("{{WeeklyY}}", weeklyY)
                    .Replace("{{CharName}}", userId)
                    .Replace("{{SearchTime}}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    .Replace("{{ListRegionItem}}", outputRegionItem.ToString())
                    .Replace("{{ListSpHellItem}}", outputSpHellItem.ToString())
                    .Replace("{{ListCountSummary}}", outputListCountSummary.ToString())
                    .Replace("{{ListBaseHellItem}}", outputBaseHellItem.ToString())
                    .Replace("{{ListWeeklyItem}}", outputWeeklyItem.ToString())
                    .Replace("{{ListDailyItem}}", outputDailyItem.ToString())
                    ;

            string fileName = $".\\output\\{DateTime.Now.ToString("yyyyMMddHHmmss")}_{Regex.Replace(userId, "[^가-힣a-zA-Z0-9 ]", "")}.html";

            File.WriteAllText(fileName, outputHtml);

            // 기본 브라우저에서 HTML 파일 열기
            Process.Start(new ProcessStartInfo
            {
                FileName = fileName,
                UseShellExecute = true  // 기본 프로그램(웹 브라우저)으로 실행
            });

            lblStat.Text = "완료.";

        }

        public async Task RunAdvenAsync(string advenName)
        {
            DateTime stdt = DateTime.Now;
            
            Common.Utils.DfMaxHelper dfmax = new Common.Utils.DfMaxHelper(_dfMaxUrl);
            var charInfos = await dfmax.GetAdvenUsersAsync(advenName);

            if(charInfos == null || charInfos.Count == 0)
            {
                lblStat.Text = "모험단 정보가 조회되지 않았습니다.";
                return;
            }
            
            StringBuilder outputInfos = new StringBuilder();

            int index = 0;
            foreach (var charInfo in charInfos.OrderByDescending(x => x.Fame))
            {
                lblStat.Text = $"{++index} / {charInfos.Count} 조회중";
                
                // 던파 api 조회
                Common.Utils.DnfApiHelper dnfApi = new Common.Utils.DnfApiHelper(_dfApiUrl);
                Common.Models.DnfApi.CharInfo charInfoApi = await dnfApi.GetCharInfoAsync(charInfo.Name, charInfo.ServerId, true);
                Common.Models.DnfApi.EquipmentResult equipment = await dnfApi.GetEquipmentsAsync(charInfoApi.CharacterId, charInfo.ServerId, true);
                Common.Models.DnfApiCharSummary charSummary = new Common.Models.DnfApiCharSummary(charInfoApi, equipment);

                Console.WriteLine($"{charInfo.Name} {(DateTime.Now - stdt).TotalSeconds}");

                // 융합석 정보 가져오기
                var charFusionItem = charSummary.GetFusionItem();


                /*
                 * <th>캐릭터명</th>
				<th>세트 이름</th>
				<th>세트 포인트</th>
				<th>세트 등급</th>
				<th>다음 필요 포인트</th>
                 */
                outputInfos.AppendLine($"<tr>");
                //<div class="col-11">
                outputInfos.AppendLine($"<td><a href='{_dfMaxUrl}/character/{charInfo.ServerId}/{charInfo.CharacterKey}' target='_blank'>{charInfo.Name}</a></td>");
                outputInfos.AppendLine($"<td>{charInfo.Fame.GetValueOrDefault().ToString("#,##0")}</td>");
                outputInfos.AppendLine($"<td>{charSummary.GetSetName()}</td>");
                outputInfos.AppendLine($"<td>{charSummary.GetSetPoint()}</td>");
                outputInfos.AppendLine($"<td class='{CodeHelper.GetRarityColor(charSummary.GetSetGrade())}'>{charSummary.GetSetGrade()}</td>");
                outputInfos.AppendLine(charSummary.GetUseItemSummaryHtmlByAdven());
                outputInfos.AppendLine(charSummary.GetFusionItemHtmlByAdven());
                outputInfos.AppendLine($"<td>{charSummary.GetNextSetPoint()}</td>");

                outputInfos.AppendLine($"</tr>");
            }

            string htmlDoc = File.ReadAllText("layoutAdven.txt");
            string outputHtml = htmlDoc
                .Replace("{{CharsSummary}}", outputInfos.ToString())
                .Replace("{{SearchTime}}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
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

        public async Task RunGuildAsync(string guildName)
        {
            DateTime stdt = DateTime.Now;
            // 던담 정보 조회
            Common.Utils.DfMaxHelper dfmax = new Common.Utils.DfMaxHelper(_dfMaxUrl);
            List<Common.Models.DfMax.CharacterInfo> guildUsers = await dfmax.GetGuildUsersAsync(guildName);

            if(guildUsers == null || guildUsers.Count == 0)
            {
                lblStat.Text = "길드 정보가 조회되지 않았습니다.";
                return;
            }

            StringBuilder outputInfos = new StringBuilder();

            int index = 0;

            var searchTarget = guildUsers.Take(30);

            int rank = 1;
            foreach (var charInfo in searchTarget)
            {
                lblStat.Text = $"{++index} / {searchTarget.Count()} 조회중";
                //Common.Models.DfDunDam.CharDetailInfo charDetailInfo = await dundam.GetCharDetailInfoAsync(charInfo.CharacterKey, charInfo.ServerId);
                //Common.Models.CharSummary charSummary = new CharSummary(charInfo, charDetailInfo);

                // 던파 api 조회
                Common.Utils.DnfApiHelper dnfApi = new Common.Utils.DnfApiHelper(_dfApiUrl);
                Common.Models.DnfApi.CharInfo charInfoApi = await dnfApi.GetCharInfoAsync(charInfo.Name, charInfo.ServerId, true);
                if (charInfoApi == null) continue;
                Common.Models.DnfApi.EquipmentResult equipment = await dnfApi.GetEquipmentsAsync(charInfoApi.CharacterId, charInfo.ServerId, true);
                Common.Models.DnfApiCharSummary charSummary = new Common.Models.DnfApiCharSummary(charInfoApi, equipment);

                Console.WriteLine($"{charInfo.Name} {(DateTime.Now - stdt).TotalSeconds}");

                /*
                 * <th>캐릭터명</th>
				<th>세트 이름</th>
				<th>세트 포인트</th>
				<th>세트 등급</th>
				<th>다음 필요 포인트</th>
                 */
                outputInfos.AppendLine($"<tr>");
                //<div class="col-11">
                outputInfos.AppendLine($"<td>{rank++}</td>");
                outputInfos.AppendLine($"<td><a href='{_dfMaxUrl}/character/{charInfo.ServerId}/{charInfo.CharacterKey}' target='_blank'>{charInfo.Name}</a></td>");
                outputInfos.AppendLine($"<td>{charInfo.Fame.GetValueOrDefault().ToString("#,##0")}</td>");
                outputInfos.AppendLine($"<td>{charSummary.GetSetName()}</td>");
                outputInfos.AppendLine($"<td>{charSummary.GetSetPoint()}</td>");
                outputInfos.AppendLine($"<td class='{CodeHelper.GetRarityColor(charSummary.GetSetGrade())}'>{charSummary.GetSetGrade()}</td>");
                outputInfos.AppendLine($"<td>{charSummary.GetNextSetPoint()}</td>");
                outputInfos.AppendLine($"<td>{(charInfo.Damage.HasValue ? $"데미지 : {charInfo.Damage.Value.ToString("#,##0")}" : $"버프력 : {charInfo.Buff.Value.ToString("#,##0")}" )}</td>");

                outputInfos.AppendLine($"</tr>");
            }

            string htmlDoc = File.ReadAllText("layoutGuild.txt");
            string outputHtml = htmlDoc
                .Replace("{{CharsSummary}}", outputInfos.ToString())
                .Replace("{{SearchTime}}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                ;

            string fileName = $".\\output\\{DateTime.Now.ToString("yyyyMMddHHmmss")}_{Regex.Replace(guildName, "[^가-힣a-zA-Z0-9 ]", "")}.html";

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
            cbServer.Items.AddRange(ServerList.ToArray());

            this.Text += $"({Application.ProductVersion})";

            if(await CheckNewVersion())
            {
                lblStat.Text += Environment.NewLine + "새로운 버전이 있습니다. 업데이트를 확인해주세요.";
            }
        }

        private async Task<bool> CheckNewVersion()
        {
            try
            {
                GithubHelper helper = new GithubHelper("https://raw.githubusercontent.com/");
                var version = await helper.GetLastVersionAsync();
                Console.WriteLine(version);

                Version currentVersion = new Version(Application.ProductVersion);

                var newVersion = new Version(version);
                if (currentVersion.CompareTo(newVersion) < 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch { return false; }
        }
    }
}
