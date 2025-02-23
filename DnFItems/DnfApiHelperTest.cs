using Common.Models;
using Common.Models.DnfApi;
using Common.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DnFItems
{
    [TestClass]
    public class DnfApiHelperTest
    {
        [TestMethod]
        public async Task GetCharInfoTest()
        {
            DnfApiHelper helper = new DnfApiHelper("https://api.neople.co.kr");
            var charInfo = await helper.GetCharInfoAsync("비상넨가", "디레지에");
            if(charInfo != null)
            {
                Console.WriteLine($"{charInfo.CharacterId} {charInfo.ServerId} {charInfo.CharacterName} {charInfo.Fame}");
            }
        }

        [TestMethod]
        public async Task GetEquipmentsTest()
        {
            DnfApiHelper helper = new DnfApiHelper("https://api.neople.co.kr");
            var charInfo = await helper.GetCharInfoAsync("비상넨가", "디레지에");
            if (charInfo != null)
            {
                var result = await helper.GetEquipmentsAsync(charInfo.CharacterId, "디레지에");
                if (result != null)
                {
                    foreach (var item in result.EquipmentInfos)
                    {
                        Console.WriteLine($"{item.SlotId} {item.ItemName} {item.ItemRarity}");
                    }
                }
            }
        }

        [TestMethod]
        public async Task GetTimeLineTest()
        {
            DnfApiHelper helper = new DnfApiHelper("https://api.neople.co.kr");
            DateTime stDate = DateTime.Now.AddDays(-7);
            DateTime edDate = DateTime.Now;
            var charInfo = await helper.GetCharInfoAsync("비상넨가", "디레지에");
            if (charInfo != null)
            {
                var result = await helper.GetTimeLineAsync(charInfo.CharacterId, "디레지에", stDate, edDate);
                if (result != null)
                {
                    foreach (var item in result.TimeLine.Rows)
                    {
                        Console.WriteLine($"{item.Name} {item.Data.DungeonName} {item.Data.ItemRarity} {item.Data.ChannelName} {item.Data.ItemName}");
                    }
                }
            }
        }

        [TestMethod]
        public async Task GetAllTimeLineTest()
        {
            DnfApiHelper helper = new DnfApiHelper("https://api.neople.co.kr");

            // 중천 update 일자
            DateTime updateDate = new DateTime(2025, 1, 9);
            DateTime stDate = updateDate;

            var charInfo = await helper.GetCharInfoAsync("비상넨가", "디레지에");
            if (charInfo != null)
            {
                do
                {
                    DateTime edDate = stDate.AddDays(7);
                    if (edDate > DateTime.Now)
                    {
                        edDate = DateTime.Now;
                    }

                    var result = await helper.GetTimeLineAsync(charInfo.CharacterId, "디레지에", stDate, edDate);
                    if (result != null)
                    {
                        Console.WriteLine($"{stDate.ToString("yyyy-MM-dd")} ~ {edDate.ToString("yyyy-MM-dd")} : {result.TimeLine?.Rows?.Count}");
                        foreach (var item in result.TimeLine.Rows)
                        {
                            Console.WriteLine($"[{item.Date}] {item.Name} / {item.Data.DungeonName} / {item.Data.ItemRarity} / {item.Data.ChannelName} / {item.Data.ChannelNo} / {item.Data.ItemName}");
                        }
                    }

                    stDate = edDate.AddDays(1);
                } while (stDate < DateTime.Today);
            }
        }

        [TestMethod]
        public async Task Get중천셋템Test()
        {
            DnfApiHelper helper = new DnfApiHelper("https://api.neople.co.kr");

            List<TimeLineItem> list = new List<TimeLineItem>();

            // 중천 update 일자
            DateTime updateDate = new DateTime(2025, 1, 9);
            DateTime stDate = updateDate;

            var charInfo = await helper.GetCharInfoAsync(".6..........", "디레지에");
            if (charInfo != null)
            {
                do
                {
                    DateTime edDate = stDate.AddDays(7);
                    if (edDate > DateTime.Now)
                    {
                        edDate = DateTime.Now;
                    }

                    var result = await helper.GetTimeLineAsync(charInfo.CharacterId, "디레지에", stDate, edDate);
                    if (result != null)
                    {
                        Console.WriteLine($"{stDate.ToString("yyyy-MM-dd")} ~ {edDate.ToString("yyyy-MM-dd")} : {result.TimeLine?.Rows?.Count}");
                        foreach (var item in result.TimeLine.Rows)
                        {
                            //Console.WriteLine($"[{item.Date}] {item.Name} / {item.Data.DungeonName} / {item.Data.ItemRarity} / {item.Data.ChannelName} / {item.Data.ChannelNo} / {item.Data.ItemName}");
                            list.Add(new TimeLineItem().SetData(item));
                        }
                    }

                    stDate = edDate;
                } while (stDate < DateTime.Today);
            }

            foreach (var item in list)
            {
                Console.WriteLine($"[{item.Date}] {item.IsLv115Item} : {item.Slot} / {item.SetItemName} / {item.Name} / {item.DungeonName} / {item.ItemRarity} / {item.ChannelName} / {item.ChannelNo} / {item.ItemName}");
            }
        }

        [TestMethod]
        public async Task 캐릭터및장비조회Test()
        {
            DnfApiHelper helper = new DnfApiHelper("https://api.neople.co.kr");

            DateTime stDate = DateTime.Now.AddDays(-7);
            DateTime edDate = DateTime.Now;
            var charInfo = await helper.GetCharInfoAsync("비상넨가", "디레지에");
            if (charInfo != null)
            {
                var result = await helper.GetEquipmentsAsync(charInfo.CharacterId, "디레지에");
                if (result != null)
                {
                    DnfApiCharSummary summary = new DnfApiCharSummary(charInfo, result);
                    Console.WriteLine(summary.GetSetName());
                    Console.WriteLine(summary.GetSetGrade());
                    Console.WriteLine(summary.GetNextSetPoint());
                    Console.WriteLine(summary.GetUseItemTitleSummaryHtml());
                    Console.WriteLine(summary.GetUseItemSummaryHtml());

                }
            }
        }

        [TestMethod]
        public async Task 아이템조회Test()
        {
            DnfApiHelper helper = new DnfApiHelper("https://api.neople.co.kr");

            var itemInfos = await helper.GetItemInfoAsync("황금향", "레전더리");
            if (itemInfos != null && itemInfos.Rows.Count > 0)
            {
                foreach (var item in itemInfos.Rows)
                {
                    Console.WriteLine($"{item.ItemName} / {item.ItemRarity} / {item.ItemTypeDetail}");
                }
            }
        }

        [TestMethod]
        public async Task 아이템조회_등급별Test()
        {
            DnfApiHelper helper = new DnfApiHelper("https://api.neople.co.kr");
            List<string> itemRaritys = new List<string>() { "레전더리", "에픽", "태초" };

            List<ItemInfo> allSetItems = new List<ItemInfo>();

            
            foreach (var rarity in itemRaritys)
            {
                var itemInfos = await helper.GetItemInfoAsync("황금향", rarity);
                if (itemInfos != null && itemInfos.Rows.Count > 0)
                {
                    allSetItems.AddRange(itemInfos.Rows);
                }
            }

            foreach (var item in allSetItems)
            {
                Console.WriteLine($"{item.ItemName} / {item.ItemRarity} / {item.ItemTypeDetail}");
            }
        }

        [TestMethod]
        public async Task 아이템조회_ALL셋트_등급별Test()
        {
            DnfApiHelper helper = new DnfApiHelper("https://api.neople.co.kr");
            List<string> itemRaritys = new List<string>() { "레전더리", "에픽", "태초" };

            List<ItemInfo> allSetItems = new List<ItemInfo>();

            foreach(var setItemWord in CodeHelper.SetItemWords)
            {
                foreach (var rarity in itemRaritys)
                {
                    Console.WriteLine($"{setItemWord}, {rarity} => ");
                    var itemInfos = await helper.GetItemInfoAsync(setItemWord, rarity);
                    if (itemInfos != null && itemInfos.Rows.Count > 0)
                    {
                        Console.WriteLine($"{itemInfos.Rows.Count}");
                        allSetItems.AddRange(itemInfos.Rows);
                    }
                }
            }
            

            //foreach (var item in allSetItems)
            //{
            //    Console.WriteLine($"{item.ItemName} / {item.ItemRarity} / {item.ItemTypeDetail}");
            //}

            System.IO.File.WriteAllText("SetItem.json", JsonConvert.SerializeObject(allSetItems));
        }
    }
}
