using Common.Models;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services
{
    public static class DnfApiService
    {
        private static string _dfApiUrl = "https://api.neople.co.kr";

        public static async Task<List<TimeLineItem>> GetAllTimeLineAsync(string userId, string serverName)
        {
            Common.Utils.DnfApiHelper dnfApiHelper = new Common.Utils.DnfApiHelper(_dfApiUrl);
            List<TimeLineItem> list = new List<TimeLineItem>();

            // 중천 update 일자
            DateTime updateDate = new DateTime(2025, 1, 9);
            DateTime stDate = updateDate;

            var charInfo = await dnfApiHelper.GetCharInfoAsync(userId, serverName);
            if (charInfo != null)
            {
                do
                {
                    DateTime edDate = stDate.AddDays(7);
                    if (edDate > DateTime.Now)
                    {
                        edDate = DateTime.Now;
                    }

                    var result = await dnfApiHelper.GetTimeLineAsync(charInfo.CharacterId, serverName, stDate, edDate);
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

            return list.Where(x => x.IsLv115Item).ToList();
        }
    }
}
