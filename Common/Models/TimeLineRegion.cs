using Common.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Common.Models
{
    public class TimeLineRegion
    {
        public int Code { get; set; }
        public string Date { get; set; }
        /// <summary>
        /// 레기온 클리어
        /// </summary>
        public string Name { get; set; }
        public string RegionName { get; set; }

        public TimeLineRegion SetData(Common.Models.DnfApi.TimeLineRow row)
        {
            if (row != null)
            {
                Code = row.Code;
                Date = row.Date;
                Name = row.Name;
                
                if(row.Data != null)
                {
                    RegionName = row.Data.RegionName;
                }
            }
            return this;
        }
    }
}
