using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.DfDunDam
{
    public class CharInfo
    {
        /*
         // 딜러
          "baseJob": "격투가(여)",
      "switching": "10Lv 3",
      "server": "diregie",
      "setPoint": "1335",
      "bakal": 13,
      "adventrueName": "이건머임s",
      "ozma": "3 억 5489 만",
      "advenBakal": 159,
      "skillDamage": "5.0",
      "name": "자취방올래?",
      "cri": "122.5",
      "fame": "42838",
      "job": "眞 스트리트파이터",
      "key": "e8d206003a1ce7a42137f9c12118f1ca"

        // 
        baseJob": "프리스트(여)",
      "server": "diregie",
      "setPoint": "1215",
      "bakal": 8,
      "adventrueName": "이건머임s",
      "advenBakal": 159,
      "skillDamage": "2.0",
      "name": "보노즈서포트",
      "buffScore": "2,510,747",
      "cri": "83.5",
      "fame": "39905",
      "job": "眞 크루세이더",
      "key": "08675288b24a2eb2d6fe8b54e4b69a04"

        // 인첸 버퍼
        "baseJob": "마법사(여)",
      "server": "diregie",
      "setPoint": "1545",
      "bakal": 11,
      "adventrueName": "이건머임s",
      "advenBakal": 159,
      "skillDamage": "11.0",
      "buffScore4": "2,876,144",
      "buffScore3": "2,956,956",
      "name": "터진솜",
      "buffScore": "3,199,393",
      "cri": "89.5",
      "fame": "43774",
      "job": "眞 인챈트리스",
      "key": "47481a771b59d30e448d7e5dde0a8aa4"
         */

        [JsonProperty("key")]
        public string CharacterKey { get; set; }
        [JsonProperty("server")]
        public string ServerId { get; set; }
        /// <summary>
        /// 딜러용 데미지
        /// </summary>
        [JsonProperty("ozma")]
        public string Damage { get; set; }
        [JsonProperty("buffScore")]
        public string BuffScore { get; set; }
        /// <summary>
        /// 인첸용 4인 버프 점수
        /// </summary>
        [JsonProperty("buffScore4")]
        public string BuffScore4 { get; set; }
        [JsonProperty("setPoint")]
        public string SetPoint { get; set; }
        [JsonProperty("job")]
        public string Job { get; set; }

        public string DamageOrBuff
        {
            get
            {
                // ozma 값이 없으면 buffScore를 사용
                if (string.IsNullOrEmpty(Damage) == false) return Damage;
                if (string.IsNullOrEmpty(BuffScore4) == false) return BuffScore4;
                return BuffScore;
            }
        }

    }
}
