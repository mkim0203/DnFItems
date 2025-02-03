using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.DfGear
{
    public class CharInfo
    {
        /*
         * "rows": [
        {
            "characterId": "5d5a92dcd47d39528d1bebb245ce6a30",
            "characterName": "상자속냥이",
            "adventureName": "이건머임s",
            "jobGrowName": "眞 비질란테",
            "fame": 46466,
            "serverId": "diregie",
            "legnd": 25,
            "epic": 8,
            "begin": 0,
            "uptime": "2025-01-31 22:53"
        }
    ]
        */

        [JsonProperty("characterId")]
        public string CharacterId { get; set; }
        [JsonProperty("characterName")]
        public string CharacterName { get; set; }
        [JsonProperty("serverId")]
        public string ServerId { get; set; }
        
    }
}
