using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.DfDunDam
{
    public class CharResult
    {
        /*
         * {
  "characters": [
    {
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
    }
  ]
}
         */

        [JsonProperty("characters")]
        public List<CharInfo> CharInfos = new List<CharInfo>();
    }
}
