using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.DnfApi
{
    public class ItemInfo
    {
        /*
        {
            "itemId": "ee160e4221f89f704bf3b361bc7e536a",
            "itemName": "화려한 황금향의 이면 - 귀걸이",
            "itemRarity": "레전더리",
            "itemTypeId": "87ff665868ffd50f8aef7948548439bd",
            "itemType": "추가장비",
            "itemTypeDetailId": "601834074c49bb0e48cb65a75a8667bc",
            "itemTypeDetail": "귀걸이",
            "itemAvailableLevel": 115,
            "fame": 1200
        }
         */

        public string ItemId { get; set; }
        /// <summary>
        /// 아이템 이름
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 아이템 등급
        /// </summary>
        public string ItemRarity { get; set; }
        public string ItemTypeId { get; set; }
        public string ItemType { get; set; }
        public string ItemTypeDetailId { get; set; }
        /// <summary>
        /// 아이템 장비 슬롯
        /// </summary>
        public string ItemTypeDetail { get; set; }
        public int? ItemAvailableLevel { get; set; }
        public int? Fame { get; set; }
    }

    public class ItemInfoResult
    {
        [JsonProperty("rows")]
        public List<ItemInfo> Rows { get; set; }
    }
}
