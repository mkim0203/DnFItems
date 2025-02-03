using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.DfGear
{
    public class TimeLineResult
    {
        [JsonProperty("rows")]
        public List<CharInfo> CharInfos { get; set; }
        [JsonProperty("timeline")]
        public List<TimeLineInfo> TimeLineInfos { get; set; }
    }
}
