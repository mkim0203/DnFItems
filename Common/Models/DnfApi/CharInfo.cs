using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.DnfApi
{
    public class CharInfo
    {
        /*
            "serverId": "diregie",
            "characterId": "20c1f6c6cb35ea4fb41bdb24bbf21ba1",
            "characterName": "비상넨가",
            "level": 115,
            "jobId": "a7a059ebe9e6054c0644b40ef316d6e9",
            "jobGrowId": "37495b941da3b1661bc900e68ef3b2c6",
            "jobName": "격투가(여)",
            "jobGrowName": "眞 넨마스터",
            "fame": 50549
         */
        /// <summary>
        /// 서버 id
        /// </summary>
        [JsonProperty("serverId")]
        public string ServerId { get; set; }
        /// <summary>
        /// 캐릭터 key
        /// </summary>
        [JsonProperty("characterId")]
        public string CharacterId { get; set; }
        [JsonProperty("characterName")]
        public string CharacterName { get; set; }
        [JsonProperty("level")]
        public int Level { get; set; }
        [JsonProperty("jobId")]
        public string JobId { get; set; }
        [JsonProperty("jobGrowId")]
        public string JobGrowId { get; set; }
        [JsonProperty("jobName")]
        public string JobName { get; set; }
        /// <summary>
        /// 전직 명
        /// </summary>
        [JsonProperty("jobGrowName")]
        public string JobGrowName { get; set; }
        /// <summary>
        /// 명성
        /// </summary>
        [JsonProperty("fame")]
        public int Fame { get; set; }

    }

    public class CharInfoResult
    {
        [JsonProperty("rows")]
        public List<CharInfo> CharInfos = new List<CharInfo>();
    }
}
