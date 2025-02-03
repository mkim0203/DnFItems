using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.DfGear
{
    public class CharResult
    {
        [JsonProperty("rows")]
        public List<CharInfo> CharInfos { get; set; }
    }
}
