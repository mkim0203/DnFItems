using Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnFItems
{
    [TestClass]
    public class DfGearHelperTest
    {
        public string _dfGearUrl = "https://api.dfgear.xyz";

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

        [TestMethod]
        public async Task 기어_timeLine_조회()
        {
            string userId = "리버핏";
            string serverName = "카인";
            Common.Utils.DfGearHelper gear = new Common.Utils.DfGearHelper(_dfGearUrl);
            List<Common.Models.DfGear.ItemDetail> result = await gear.GetTimeLineItems(userId, serverName);

            if (result != null) {
                //foreach (var item in result) {
                //    Console.WriteLine($"{item.ItemName} / {item.SetItemName} / {item.ItemRarity} / {item.ItemType}");
                //}


                Console.WriteLine("===============");
                // SetItemName 기준으로 그룹화
                var groupedItems = result.Where(x => string.IsNullOrEmpty(x.ConvertSetItem) == false).OrderBy(item => item.ItemType).ThenByDescending(item => item.ItemRarityLevel).GroupBy(item => item.ConvertSetItem);

                // 출력
                //foreach (var group in groupedItems)
                //{
                //    Console.WriteLine($"세트 이름: {group.Key}");
                //    foreach (var item in group)
                //    {
                //        Console.WriteLine($"  - {item.ItemName} / {item.ItemRarity} / {item.ItemType} / {item.SetLevel} / {item.SetPoint}");
                //    }
                //    Console.WriteLine();
                //}

                StringBuilder outputListSetItem = new StringBuilder();
                foreach (var group in groupedItems)
                {
                    outputListSetItem.AppendLine($"<h4>{group.Key}</h4>");
                    //<div class="col-11">
                    outputListSetItem.AppendLine($"<div class='col-8'>");
                    outputListSetItem.AppendLine($"<table class='table table-bordered'>{group.Key}");
                    outputListSetItem.AppendLine($"<tr><th>이름</th><th>등급</th><th>부위</th></tr>");
                    foreach (var item in group)
                    {
                        outputListSetItem.AppendLine($"<tr><td>{item.ItemName}</td><td>{item.ItemRarity}</td><td>{item.ItemType}</td></tr>");
                    }
                    outputListSetItem.AppendLine($"</table></div><br/>");
                }

                // 머리어깨, 상의, 하의, 벨트, 신발, 팔찌, 목걸이, 보조장비, 반지, 귀걸이, 마법석

                //Console.WriteLine("==================");
                // ConvertSetItem & ItemType 기준으로 그룹화 후 SetPoint가 가장 높은 항목 선택
                var bestItems = result
                    .Where(item => string.IsNullOrEmpty(item.ConvertSetItem) == false)
                    .GroupBy(item => new { item.ConvertSetItem, item.ItemType })
                    .Select(group => group.OrderByDescending(item => item.SetPoint).First())
                    .OrderBy(x => x.ConvertSetItem)
                    .ToList();

                //// 출력
                //foreach (Common.Models.DfGear.ItemDetail item in bestItems)
                //{
                //    Console.WriteLine($"세트: {item.ConvertSetItem}, 타입: {item.ItemType}, 아이템: {item.ItemName}, SetPoint: {item.SetPoint}");
                //}

                // 고유 아이템
                List<Common.Models.DfGear.ItemDetail> commonItems = bestItems.Where(x => x.ConvertSetItem == "고유").ToList();

                List<AvailableSetItem> allAvailableSetItem = new List<AvailableSetItem>();
                foreach (string setName in SetItems)
                {
                    AvailableSetItem addItem = new AvailableSetItem() { SetItemName = setName };
                    foreach(Common.Models.DfGear.ItemDetail item in bestItems.Where(x => x.SetItemName == setName))
                    {
                        addItem.SettingPoint(item);
                    }
                    foreach (Common.Models.DfGear.ItemDetail item in commonItems)
                    {
                        addItem.SettingPoint(item);
                    }

                    allAvailableSetItem.Add(addItem);
                }

                //Console.WriteLine("==================");
                //Console.WriteLine("셋트명 : 머리어깨 상의 하의 벨트 신발 // 팔찌 목걸이 보조장비 반지 귀걸이 마법석");
                //// 셋트 별로 출력.
                //foreach (var setItem in allAvailableSetItem) {
                //    Console.WriteLine(setItem);
                //}

                StringBuilder outputAvailableSetItem = new StringBuilder();

                //Console.WriteLine("==================");
                //Console.WriteLine("<table>");
                //// 셋트 별로 출력.
                //foreach (var setItem in allAvailableSetItem)
                //{
                //    Console.WriteLine(setItem.OutputHtml());
                //}
                //Console.WriteLine("</table>");

                foreach (var setItem in allAvailableSetItem)
                {
                    outputAvailableSetItem.AppendLine(setItem.OutputHtml());
                }


                string htmlDoc = File.ReadAllText("layout.txt");
                string outputHtml = htmlDoc.Replace("{{CharInfo}}", $"{userId} / {serverName}")
                        .Replace("{{ListSetItem}}", outputListSetItem.ToString())
                        .Replace("{{AvailableSetItem}}", outputAvailableSetItem.ToString());

                Console.WriteLine("----------------");
                Console.WriteLine(outputHtml);

            }

        }
    }
}
