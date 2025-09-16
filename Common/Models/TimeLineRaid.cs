using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class TimeLineRaid
    {
        public int Code { get; set; }
        public string Date { get; set; }
        /// <summary>
        /// 레기온 클리어
        /// </summary>
        public string Name { get; set; }
        public string RaidName { get; set; }

        public TimeLineRaid SetData(Common.Models.DnfApi.TimeLineRow row)
        {
            if (row != null)
            {
                Code = row.Code;
                Date = row.Date;
                Name = row.Name;

                if (row.Data != null)
                {
                    RaidName = row.Data.RaidName;
                }
            }
            return this;
        }
    }
}
