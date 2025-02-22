using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.DnfApi
{
    public class EquipmentInfo
    {
        /*
        {
          "slotId": "JACKET",
          "slotName": "상의",
          "itemId": "1010061a15b07746cd5ca65c665335a6",
          "itemName": "역전의 발키리 신성 갑옷",
          "itemAvailableLevel": 115,
          "itemRarity": "레전더리",
          "setItemId": "854dc8c01b1bc231e132dae5df3c52bc",
          "setItemName": "고대 전장의 발키리 세트",
          "reinforce": 0,
          "itemGradeName": "최상급",
           
          "amplificationName": null,
          "refine": 0,
           
          "upgradeInfo": {
            "itemId": "880c5512dc47bea29d87fd3bec359390",
            "itemName": "전장 : 빛나는 발키리의 금장",
            "itemRarity": "유니크",
            "setItemId": "854dc8c01b1bc231e132dae5df3c52bc",
            "setItemName": "고대 전장의 발키리 세트",
            "setPoint": 25
          },
          "tune": {
            "level": 0,
            "setPoint": 165
          }
        },
         */
        [JsonProperty("slotId")]
        public string SlotId { get; set; }
        [JsonProperty("slotName")]
        public string SlotName { get; set; }
        [JsonProperty("itemId")]
        public string ItemId { get; set; }
        [JsonProperty("itemName")]
        public string ItemName { get; set; }
        [JsonProperty("itemAvailableLevel")]
        public int ItemAvailableLevel { get; set; }
        [JsonProperty("itemRarity")]
        public string ItemRarity { get; set; }
        [JsonProperty("setItemId")]
        public string SetItemId { get; set; }
        [JsonProperty("setItemName")]
        public string SetItemName { get; set; }
        /// <summary>
        /// 강화 or 증폭 수치
        /// </summary>
        [JsonProperty("reinforce")]
        public int Reinforce { get; set; }
        [JsonProperty("itemGradeName")]
        public string ItemGradeName { get; set; }
        /// <summary>
        /// 증폭 스텟
        /// </summary>
        [JsonProperty("amplificationName")]
        public string AmplificationName { get; set; }
        [JsonProperty("refine")]
        public int Refine { get; set; }

        [JsonProperty("upgradeInfo")]
        public EquipmentUpgradeInfo UpgradeInfo { get; set; }
        [JsonProperty("tune")]
        public EquipmentTuneInfo TuneInfo { get; set; }
    }

    public class EquipmentTuneInfo
    {
        /*
            "level": 0,
            "setPoint": 165
         */
        [JsonProperty("level")]
        public int? Level { get; set; }
        [JsonProperty("setPoint")]
        public int? SetPoint { get; set; }
    }

    public class EquipmentUpgradeInfo
    {
        /*
            "itemId": "880c5512dc47bea29d87fd3bec359390",
            "itemName": "전장 : 빛나는 발키리의 금장",
            "itemRarity": "유니크",
            "setItemId": "854dc8c01b1bc231e132dae5df3c52bc",
            "setItemName": "고대 전장의 발키리 세트",
            "setPoint": 25
         */

        [JsonProperty("itemId")]
        public string ItemId { get; set; }
        [JsonProperty("itemName")]
        public string ItemName { get; set; }
        [JsonProperty("itemRarity")]
        public string ItemRarity { get; set; }
        [JsonProperty("setItemId")]
        public string SetItemId { get; set; }
        [JsonProperty("setItemName")]
        public string SetItemName { get; set; }
        [JsonProperty("setPoint")]
        public int? SetPoint { get; set; }
    }

    public class SetItemInfoItem
    {
        /*
        {
          "setItemId": "854dc8c01b1bc231e132dae5df3c52bc",
          "setItemName": "발키리, 역전의 용사 세트",
          "setItemRarityName": "레전더리 Ⅰ",
          "active": {
            "status": [
              {
                "name": "모험가 명성",
                "value": 10000
              },
              {
                "name": "버프력",
                "value": 24000
              },
              {
                "name": "최종 데미지",
                "value": "185.1%"
              }
            ],
            "setPoint": {
              "current": 1680,
              "min": 1650,
              "max": 1735
            }
          }
        }
         */
        [JsonProperty("setItemId")]
        public string SetItemId { get; set; }
        [JsonProperty("setItemName")]
        public string SetItemName { get; set; }
        [JsonProperty("setItemRarityName")]
        public string SetItemRarityName { get; set; }
        [JsonProperty("active")]
        public SetItemActiveInfo Active { get; set; }
    }

    public class SetItemActiveInfo
    {
        /*
         "status": [
              {
                "name": "모험가 명성",
                "value": 10000
              },
              {
                "name": "버프력",
                "value": 24000
              },
              {
                "name": "최종 데미지",
                "value": "185.1%"
              }
            ],
            "setPoint": {
              "current": 1680,
              "min": 1650,
              "max": 1735
            }
         */
        [JsonProperty("setPoint")]
        public SetItemSetPointInfo SetPoint { get; set; }
    }

    public class SetItemSetPointInfo
    {
        [JsonProperty("current")]
        public int Current { get; set; }
        [JsonProperty("min")]
        public int Min { get; set; }
        [JsonProperty("max")]
        public int Max { get; set; }
    }

        /// <summary>
        /// 장비 아이템 정보
        /// </summary>
        public class EquipmentResult : CharInfo
    {
        /*
            "adventureName": "이건머임s",
            "guildId": "72792e8c38c3cb7484027532a77a73db",
            "guildName": "Nugget",
         */

        [JsonProperty("adventureName")]
        public string AdventureName { get; set; }
        [JsonProperty("guildId")]
        public string GuildId { get; set; }
        [JsonProperty("guildName")]
        public string GuildName { get; set; }

        /// <summary>
        /// 착용 장비
        /// </summary>
        [JsonProperty("equipment")]
        public List<EquipmentInfo> EquipmentInfos = new List<EquipmentInfo>();

        /// <summary>
        /// 세트 장비 정보
        /// </summary>
        [JsonProperty("setItemInfo")]
        public List<SetItemInfoItem> SetItemInfos = new List<SetItemInfoItem>();
    }
}
