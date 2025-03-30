using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class DashboardItem
    {
        public string CharName { get; set; }
        public string CharKey { get; set; }
        public int WeekNumber { get; set; }

        public AllTimeLineSummary WeekTimeLineSummary { get; set; }

        public DateTime StartDate
        {
            get
            {
                if(WeekNumber == 0)
                {
                    return CodeHelper.SeasonTheNewWave;
                }
                else
                {
                    // 기준일 + (주차 - 1주차) * 7일
                    return CodeHelper.SeasonTheNewWave.AddDays((WeekNumber - 1) * 7);
                }
            }
        }
        public DateTime EndDate
        {
            get
            {
                return StartDate.AddDays(7);
            }
        }


        public int BegCount { get; set; }
        public int EpiCount { get; set; }
        public int LegCount { get; set; }
        public int TotalCount { get { return BegCount + EpiCount + LegCount; } }

        public override string ToString()
        {
            return $"{WeekNumber}\t{TotalCount}\t{BegCount}\t{EpiCount}\t{LegCount}";
        }
    }
}
