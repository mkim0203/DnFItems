using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.DfDunDam
{
    public class CharDetailInfo
    {
        // "dealerRanking": 2957,
        [JsonProperty("dealerRanking")]
        public int? DealerRanking { get; set; }
        [JsonProperty("bufferRanking")]
        public int? BufferRanking { get; set; }
        [JsonProperty("setsPoint")]
        public int? SetsPoint {  get; set; }
        [JsonProperty("setsName")]
        public string SetsName { get; set; }
        /// <summary>
        /// 세트 등급
        /// </summary>
        [JsonProperty("setsGrade")]
        public string SetsGrade { get; set; }
        /// <summary>
        /// 명성
        /// </summary>
        [JsonProperty("fame")]
        public int fame {  get; set; }
        [JsonProperty("equip")]
        public List<EquipItem> UseItems { get; set; }

        public int Rank
        {
            get
            {
                if (BufferRanking != null) return BufferRanking.Value;
                if (DealerRanking != null) return DealerRanking.Value;
                return 0;
            }
        }


    }

    public class EquipItem
    {
        /*
         "reinforceNum": "+12",
      "lv105Leveling": "0 \/ 0",
      "code": "7_cbow",
      "lv105Explain": "",
      "siroco": "",
      "sirocoR": "",
      "sirocoReal": "",
      "refine": "4",
      "slot": "무기",
      "ozma": "",
      "explainDetail": "",
      "itemUp": 0,
      "itemid": "6e44ccf46ff077dc0ca1a93bbe36d5d5",
      "reinforceType": "강화",
      "enchant": "수속강 + 15\n암속강 + 15\n",
      "name": "프렌즈 팔케",
      "rarity": "에픽",
      "itemLevel": "115"
         */

        /// <summary>
        /// 증폭 or 강화 수치
        /// </summary>
        [JsonProperty("reinforceNum")]
        public string ReinforceNum { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// 융합장비 등급
        /// </summary>
        [JsonProperty("sirocoR")]
        public string FusionRarity { get; set; }
        /// <summary>
        /// 융합장비 이름
        /// </summary>
        [JsonProperty("sirocoReal")]
        public string FusionName { get; set; }
        [JsonProperty("slot")]
        public string Slot {  get; set; }
        
        /// <summary>
        /// 증폭 or 강화
        /// </summary>
        [JsonProperty("reinforceType")]
        public string ReinforceType { get; set; }

        [JsonProperty("rarity")]
        public string Rarity { get; set; }

        /// <summary>
        /// 조율 수치
        /// </summary>
        [JsonProperty("itemUp")]
        public string ItemUp {  get; set; }

        [JsonProperty("itemid")]
        public string Itemid { get; set; }
    }
}
