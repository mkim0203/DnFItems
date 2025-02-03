using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.DfGear
{
    public class TimeLineInfo
    {
        /*
         * "timeline": [
        {
            "code": 505,
            "date": "2025-01-10 18:04",
            "itemId": "5b8a493b5960c3a91bc6902d620416db",
            "itemName": "전설 속의 전승 - 차크라 웨펀",
            "itemRarity": "레전더리",
            "channel": "중천_44",
            "detail": {
                "itemName": "전설 속의 전승 - 차크라 웨펀",
                "setItemName": null,
                "itemRarity": "레전더리",
                "itemType": "차크라 웨펀"
            }
        },
         */

        [JsonProperty("itemId")]
        public string ItemId { get; set; }
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("detail")]
        public ItemDetail Item { get; set; }
    }

    public class ItemDetail
    {
        [JsonProperty("itemName")]
        public string ItemName {  get; set; }
        [JsonProperty("setItemName")]
        public string SetItemName { get; set; }
        [JsonProperty("itemRarity")]
        public string ItemRarity { get; set; }
        [JsonProperty("itemType")]
        public string ItemType { get; set; }

        public string ItemId { get; set; }
        public string Channel { get; set; }
        public string Date { get; set; }

        public string ConvertSetItem
        {
            get
            {
                if (ItemName.StartsWith("고유")) return "고유";
                if (string.IsNullOrWhiteSpace(SetItemName)) return "무기";
                return SetItemName;
            }
        }

        public int ItemRarityLevel
        {
            get
            {
                int setPoint = 1;
                switch (ItemRarity)
                {
                    case "레전더리":
                        setPoint = 2;
                        break;
                    case "에픽":
                        setPoint = 3;
                        break;
                    case "태초":
                        setPoint = 4;
                        break;
                    default:
                        setPoint = 1;
                        break;
                }

                return setPoint;
            }

        }

        public int? SetPoint
        {
            get
            {
                if (string.IsNullOrEmpty(ConvertSetItem)) return null;
                int setPoint = 0;
                switch(ItemRarity)
                {
                    case "레전더리":
                        setPoint = 165;
                        break;
                    case "에픽":
                        setPoint = 215;
                        break;
                    case "태초":
                        setPoint = 265;
                        break;
                    default:
                        setPoint = 125;
                        break;
                }

                return setPoint;
            }
        }
    }
}
