using Common.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Common.Models
{
    public class TimeLinePledgeItem
    {
        public TimeLinePledgeItem SetData(Common.Models.DnfApi.TimeLineRow row)
        {
            if (row != null)
            {
                Code = row.Code;
                Date = row.Date;
                Name = row.Name;
                ItemId = row.Data.ItemId;
                ItemName = row.Data.ItemName;

                // 제작서로 받은 레어리티는 정보가 달라서 이름으로 구분
                ItemRarity = PledgeCodeHelper.GetRarity(row.Data.ItemName);
                SetItemName = PledgeCodeHelper.GetSetName(row.Data.ItemName);
                ChannelName = row.Data.ChannelName;
                ChannelNo = row.Data.ChannelNo;
                DungeonName = row.Data.DungeonName;
                MistGear = row.Data.MistGear;


            }
            return this;
        }


        public int Code { get; set; }
        public string Date { get; set; }

        public int WeekNumber
        {
            get
            {
                // 기준 주차 천해천 시즌 시작일
                DateTime baseDate = PledgeCodeHelper.Season11;

                try
                {
                    // 문자열을 DateTime으로 변환
                    if (!DateTime.TryParseExact(Date, "yyyy-MM-dd HH:mm",
                                                CultureInfo.InvariantCulture,
                                                DateTimeStyles.None, out DateTime targetDate))
                    {
                        //throw new ArgumentException("잘못된 날짜 형식입니다.");
                        return 0;
                    }

                    // 기준일과의 차이 계산
                    TimeSpan difference = targetDate - baseDate;

                    // 주차 계산 (0주차부터 시작하므로 +1)
                    int weekNumber = (difference.Days / 7) + 1;

                    return weekNumber;
                }
                catch { return 0; }
            }
        }

        public string DateDay
        {
            get
            {
                try
                {
                    return Date.Substring(0, 10);
                }
                catch
                {
                    return Date;
                }
            }
        }
        public string DateMonth
        {
            get
            {
                try
                {
                    return Date.Substring(0, 7);
                }
                catch
                {
                    return Date;
                }
            }
        }

        /// <summary>
        /// 획득 방법
        /// </summary>
        public string Name { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemRarity { get; set; }
        public string ChannelName { get; set; }
        public int? ChannelNo { get; set; }
        public string DungeonName { get; set; }
        public bool? MistGear { get; set; }

        public string Slot { get; set; }
        public int SlotOrder { get; set; }
        public string SetItemName { get; set; }

        public string ItemType { get; set; }

        public bool IsPledge
        {
            get
            {
                return ItemName.Contains("서약");
            }
        }

        public string GetChannelInfo
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ChannelName) == false && ChannelNo.HasValue)
                {
                    return $"{ChannelName}_{ChannelNo.Value}";
                }
                if (string.IsNullOrWhiteSpace(DungeonName) == false) return DungeonName;
                return string.Empty;
            }
        }


        public int ItemRarityLevel
        {
            get
            {
                int level = 1;
                switch (ItemRarity)
                {
                    case "레전더리":
                        level = 2;
                        break;
                    case "에픽":
                        level = 3;
                        break;
                    case "태초":
                        level = 4;
                        break;
                    default:
                        level = 1;
                        break;
                }

                return level;
            }

        }

    }
}
