using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class TimeLineItem
    {
        public TimeLineItem SetData(Common.Models.DnfApi.TimeLineRow row)
        {
            if (row != null)
            {
                Code = row.Code;
                Date = row.Date;
                Name = row.Name;
                ItemId = row.Data.ItemId;
                ItemName = row.Data.ItemName;
                ItemRarity = row.Data.ItemRarity;
                ChannelName = row.Data.ChannelName;
                ChannelNo = row.Data.ChannelNo;
                DungeonName = row.Data.DungeonName;
                MistGear = row.Data.MistGear;

                var findItem = CodeHelper.ItemsLv115.FirstOrDefault(x => x.ItemId.Equals(row.Data.ItemId));
                if(findItem != null)
                {
                    Slot = findItem.ItemType == "무기" ? findItem.ItemType : findItem.ItemTypeDetail;
                    ItemType = findItem.ItemType;
                } else
                {
                    // 115 던전 에서 나온거면 무기로 판단함 (레거시 정보 추가하면 코드 빼도됨)
                    if (IsLv115Item == true) { Slot = "무기"; }
                }

                // 셋트 아이템 정보 구하기....
                // api로 하나씩 조회할려면 너무 많이 조회해야함
                if (string.IsNullOrEmpty(row.Data.ItemName) == false) SetItemName = GetSetName(row.Data.ItemName);
                
            }
            return this;
        }

        private string GetSetName(string itemName)
        {
            // "황금향", //"영원히 이어지는 황금향 세트",
            if (itemName.IndexOf("황금향") != -1) return "영원히 이어지는 황금향 세트";

            //"정화", //"칠흑의 정화 세트",
            if(itemName.IndexOf("정화") != -1) return "칠흑의 정화 세트";
            //"행운", //"세렌디피티 세트",
            if(itemName.IndexOf("행운") != -1) return "세렌디피티 세트";
            //"한계", //"한계를 넘어선 에너지 세트",
            if(itemName.IndexOf("한계") != -1) return "한계를 넘어선 에너지 세트";
            //"페어리", //"소울 페어리 세트",
            if (itemName.IndexOf("페어리") != -1) return "소울 페어리 세트";
            //"자연", //"압도적인 자연 세트",
            if(itemName.IndexOf("자연") != -1) return "압도적인 자연 세트";
            //"발키리", //"고대 전장의 발키리 세트",
            if(itemName.IndexOf("발키리") != -1) return "고대 전장의 발키리 세트";
            //"여우", //"에테리얼 오브 아츠 세트",
            if(itemName.IndexOf("여우") != -1) return "에테리얼 오브 아츠 세트";
            //"그림자", //"그림자에 숨은 죽음 세트",
            if(itemName.IndexOf("여우") == -1 && itemName.IndexOf("그림자") != -1) return "그림자에 숨은 죽음 세트";
            //"무리의", //"무리 사냥의 길잡이 세트",
            if(itemName.IndexOf("무리의") != -1) return "무리 사냥의 길잡이 세트";
            //"마력의", //"마력의 영역 세트",
            if(itemName.IndexOf("마력의") != -1) return "마력의 영역 세트";
            //"용제", "용왕", "용투", "용의", //"용투장의 난 세트"
            if(itemName.IndexOf("용제") != -1 
                || itemName.IndexOf("용왕") != -1 
                || itemName.IndexOf("용투") != -1 
                || itemName.IndexOf("용의") != -1) return "용투장의 난 세트";

            return string.Empty;
        }

        public int Code { get; set; }
        public string Date { get; set; }
        /// <summary>
        /// 획득 방법
        /// </summary>
        public string Name { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemRarity { get; set; }
        public string ChannelName { get; set; }
        public int? ChannelNo { get; set; }
        public string DungeonName { get; set; }
        public bool? MistGear { get; set; }

        public string Slot { get; set; }
        public string SetItemName { get; set; }

        public string ItemType { get; set; }

        public string GetChannelInfo
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ChannelName) == false && ChannelNo.HasValue)
                {
                    return $"{ChannelName}_{ChannelNo.Value}";
                }
                if (string.IsNullOrWhiteSpace(DungeonName) == false) return DungeonName;
                return string.Empty;
            }
        }

        public string ConvertSetItem
        {
            get
            {
                if (ItemName.StartsWith("고유")) return "고유";
                if (string.IsNullOrWhiteSpace(SetItemName)) return "무기";
                return SetItemName;
            }
        }

        public bool IsLv115Item
        {
            get
            {
                try
                {
                    // 아이템 이름으로 구분
                    bool findItem = CodeHelper.SetItemWords.Any(x => ItemName.Contains(x));
                    if(findItem) return true;
                    // 던전 이름으로 구분
                    bool findDungeon = CodeHelper.AllDungeonNames.Any(x => DungeonName.Equals(x));
                    if(findDungeon) return true;

                    // 레거시 검색..

                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        public int ItemRarityLevel
        {
            get
            {
                int level = 1;
                switch (ItemRarity)
                {
                    case "레전더리":
                        level = 2;
                        break;
                    case "에픽":
                        level = 3;
                        break;
                    case "태초":
                        level = 4;
                        break;
                    default:
                        level = 1;
                        break;
                }

                return level;
            }

        }

        public int? SetPoint
        {
            get
            {
                if (string.IsNullOrEmpty(ConvertSetItem)) return null;
                int setPoint = 0;
                switch (ItemRarity)
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

        public bool IsBaseHell
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(DungeonName)) return false;
                    // 헬던전 인지
                    bool findDungeon = DungeonName.Equals("종말의 숭배자");
                    if (findDungeon) return true;

                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool IsSpHell
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(DungeonName)) return false;
                    // 헬던전 인지
                    bool findDungeon = DungeonName.Equals("심연 : 종말의 숭배자");
                    if (findDungeon) return true;

                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool IsWeekly
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(DungeonName)) return false;
                    // 던전 이름으로 구분
                    bool findDungeon = CodeHelper.WeeklyDungeonNames.Any(x => DungeonName.Equals(x));
                    if (findDungeon) return true;

                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
