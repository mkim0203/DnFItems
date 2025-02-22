using Common.Models;
using Common.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
    }
}
