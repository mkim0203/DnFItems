using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.DnfApi
{
    public class TimeLineRow
    {
        /*
         {
            "code": 513,
            "name": "아이템 획득(던전 카드 보상)",
            "date": "2025-02-18 19:24",
            "data": {
              "itemId": "a072b7e6b059574e8b6a8a7dadd8d72f",
              "itemName": "화려한 황금향의 축복 - 목걸이",
              "itemRarity": "레전더리",
              "dungeonName": "모독 : 일렁이는 군도",
              "mistGear": false
            }
          },
          {
            "code": 505,
            "name": "아이템 획득(던전 드랍)",
            "date": "2025-02-17 17:21",
            "data": {
              "itemId": "1064578619f203193e5cb55ca9909824",
              "itemName": "수습 여우의 매혹 반지",
              "itemRarity": "레전더리",
              "channelName": "마계",
              "channelNo": 10,
              "dungeonName": "종말의 숭배자",
              "mistGear": false
            }
          },
         */

        public int Code { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public TimeLineData Data { get; set; }
    }

    public class TimeLineData
    {
        /*
            "data": {
                "itemId": "1064578619f203193e5cb55ca9909824",
                "itemName": "수습 여우의 매혹 반지",
                "itemRarity": "레전더리",
                "channelName": "마계",
                "channelNo": 10,
                "dungeonName": "종말의 숭배자",
                "mistGear": false
            }
         */
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemRarity { get; set; }
        public string ChannelName { get; set; }
        public int? ChannelNo { get; set; }
        public string DungeonName { get; set; }
        public bool? MistGear { get; set; }
    }

    public class TimeLineSummary
    {
        [JsonProperty("rows")]
        public List<TimeLineRow> Rows { get; set; }
    }
    public class TimeLineResult : CharInfo
    {
        /*
            "serverId": "diregie",
          "characterId": "0e20462c3738c068d41247e05a2b4842",
          "characterName": "비상마커",
          "level": 115,
          "jobId": "b9cb48777665de22c006fabaf9a560b3",
          "jobGrowId": "6d459bc74ba73ee4fe5cdc4655400193",
          "jobName": "아처",
          "jobGrowName": "眞 헌터",
          "fame": 52221,
          "adventureName": "이건머임s",
          "guildId": "72792e8c38c3cb7484027532a77a73db",
          "guildName": "Nugget",
          "timeline": {
            "date": {
              "start": "2025-02-01 00:00",
              "end": "2025-02-22 00:00"
            },
            "next": null,
            "rows": [
              {
                "code": 513,
                "name": "아이템 획득(던전 카드 보상)",
                "date": "2025-02-20 23:10",
                "data": {
                  "itemId": "6069dda08b7d65473814030a579b102f",
                  "itemName": "은은한 마력의 영역 반지",
                  "itemRarity": "레전더리",
                  "dungeonName": "침묵의 성소",
                  "mistGear": false
                }
              }
            ]
           }
         */
        [JsonProperty("adventureName")]
        public string AdventureName { get; set; }
        [JsonProperty("guildId")]
        public string GuildId { get; set; }
        [JsonProperty("guildName")]
        public string GuildName { get; set; }

        [JsonProperty("timeline")]
        public TimeLineSummary TimeLine { get; set; }

    }

}
