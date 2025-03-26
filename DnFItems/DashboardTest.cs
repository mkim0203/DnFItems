using Common.Models;
using Common.Services;
using Common.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnFItems
{
    [TestClass]
    public class DashboardTest
    {
        public string _dfMaxUrl = "https://dfmax.xyz";

        [TestMethod]
        public async Task TestMethod1()
        {
            Common.Utils.DfMaxHelper dfmax = new Common.Utils.DfMaxHelper(_dfMaxUrl);
            var charInfos = await dfmax.GetAdvenUsersAsync("이건머임s");

            if (charInfos == null || charInfos.Count == 0)
            {
                return;
            }


            List<DashboardItem> hellDashboardItems = new List<DashboardItem>();

            int curWeekNumber = DateTime.Now.WeekNumber();

            foreach (var charInfo in charInfos.OrderByDescending(x => x.Fame))
            {
               
                List<TimeLineItem> itemTimeLine = await DnfApiService.GetAllTimeLineAsync(charInfo.Name, charInfo.ServerId, true);

                AllTimeLineSummary summary = new AllTimeLineSummary(itemTimeLine);

                
                for (int i = 1; i <= curWeekNumber; i++)
                {
                    var weekItems = itemTimeLine.Where(x => x.WeekNumber == i).ToList();
                    hellDashboardItems.Add(new DashboardItem()
                    {
                        CharName = charInfo.Name,
                        WeekNumber = i,
                        BegCount = summary.AllBaseHellBeg.Where(x => x.WeekNumber == i).Count(),
                        EpiCount = summary.AllBaseHellEpi.Where(x => x.WeekNumber == i).Count(),
                        LegCount = summary.AllBaseHellLeg.Where(x => x.WeekNumber == i).Count()
                    });
                }

            }

            Console.WriteLine("###############");
            foreach (var item in hellDashboardItems)
            {
                Console.WriteLine(item.CharName + "\t" + item.ToString());
            }

            Console.WriteLine("###############");
            for (int i = 1; i <= curWeekNumber; i++)
            {
                var temp = hellDashboardItems.Where(x => x.WeekNumber == i);
                Console.WriteLine($"[{temp.First().StartDate.ToString("yyyy-MM-dd HH:mm")}~{temp.First().EndDate.ToString("yyyy-MM-dd HH:mm")}] {i}\t{temp.Sum(x => x.TotalCount)}\t{temp.Sum(x => x.BegCount)}\t{temp.Sum(x => x.EpiCount)}\t{temp.Sum(x => x.LegCount)}");
            }

        }
    }
}
