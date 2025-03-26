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

        /// <summary>
        /// 타임라인 - 아이템 획득 정보
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public static async Task<List<TimeLineItem>> GetAllTimeLineAsync(string userId, string serverNameOrId, bool isServerId = false)
        {
            Common.Utils.DnfApiHelper dnfApiHelper = new Common.Utils.DnfApiHelper(_dfApiUrl);
            List<TimeLineItem> list = new List<TimeLineItem>();

            // 중천 update 일자
            DateTime updateDate = new DateTime(2025, 1, 9);
            DateTime stDate = updateDate;

            var charInfo = await dnfApiHelper.GetCharInfoAsync(userId, serverNameOrId, isServerId);
            if (charInfo != null)
            {
                do
                {
                    DateTime edDate = stDate.AddDays(7);
                    if (edDate > DateTime.Now)
                    {
                        edDate = DateTime.Now;
                    }

                    var result = await dnfApiHelper.GetTimeLineAsync(charInfo.CharacterId, serverNameOrId, stDate, edDate, isServerId);
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

        /// <summary>
        /// 타임라인 - 레기온 클리어 정보
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public static async Task<List<TimeLineRegion>> GetAllTimeLineRegionAsync(string userId, string serverName)
        {
            Common.Utils.DnfApiHelper dnfApiHelper = new Common.Utils.DnfApiHelper(_dfApiUrl);
            List<TimeLineRegion> list = new List<TimeLineRegion>();

            // 중천 update 일자
            DateTime updateDate = new DateTime(2025, 1, 9);
            DateTime stDate = updateDate;

            var charInfo = await dnfApiHelper.GetCharInfoAsync(userId, serverName);
            if (charInfo != null)
            {
                do
                {
                    // 레기온 클리어 정보는 1달치 조회
                    DateTime edDate = stDate.AddMonths(1);
                    if (edDate > DateTime.Now)
                    {
                        edDate = DateTime.Now;
                    }

                    var result = await dnfApiHelper.GetTimeLineRegionAsync(charInfo.CharacterId, serverName, stDate, edDate);
                    if (result != null)
                    {
                        Console.WriteLine($"{stDate.ToString("yyyy-MM-dd")} ~ {edDate.ToString("yyyy-MM-dd")} : {result.TimeLine?.Rows?.Count}");
                        foreach (var item in result.TimeLine.Rows)
                        {
                            //Console.WriteLine($"[{item.Date}] {item.Name} / {item.Data.DungeonName} / {item.Data.ItemRarity} / {item.Data.ChannelName} / {item.Data.ChannelNo} / {item.Data.ItemName}");
                            list.Add(new TimeLineRegion().SetData(item));
                        }
                    }

                    stDate = edDate;
                } while (stDate < DateTime.Today);
            }

            return list.Where(x => x.Code == 209 && x.RegionName.Equals("베누스")).ToList();
        }
    }
}
