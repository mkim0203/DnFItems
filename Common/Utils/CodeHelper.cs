using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public static class CodeHelper
    {
        public static string GetServerId(string serverName)
        {
            string serverId = "";
            switch (serverName)
            {
                case "카인":
                    serverId = "cain";
                    break;
                case "디레지에":
                    serverId = "diregie";
                    break;
                case "시로코":
                    serverId = "siroco";
                    break;
                case "프레이":
                    serverId = "prey";
                    break;
                case "카시야스":
                    serverId = "casillas";
                    break;
                case "힐더":
                    serverId = "hilder";
                    break;
                case "안톤":
                    serverId = "anton";
                    break;
                case "바칼":
                    serverId = "bakal";
                    break;
                case "모험단":
                    serverId = "adven";
                    break;
            }

            return serverId;
        }

        public static string GetRarityColor(string targetRarity)
        {
            if (string.IsNullOrWhiteSpace(targetRarity)) return string.Empty;
            if (targetRarity.StartsWith("유니크")) return "uni";
            if (targetRarity.StartsWith("레전더리")) return "leg";
            if (targetRarity.StartsWith("에픽")) return "epi";
            if (targetRarity.StartsWith("태초")) return "beg";
            return "";
        }

        public static string GetFusionRarityColor(int setPoint)
        {
            string retValue = "";
            switch(setPoint)
            {
                case 25:
                    retValue = "uni";
                    break;
                case 65:
                    retValue = "epi";
                    break;
                default:
                    break;

            }
            return retValue;
        }

        public static Dictionary<string, int> RarityCodes = new Dictionary<string, int>()
        {
            { RarityCode.유니크Ⅰ.ToString(), (int)RarityCode.유니크Ⅰ },
            { RarityCode.유니크Ⅱ.ToString(), (int)RarityCode.유니크Ⅱ },
            { RarityCode.유니크Ⅲ.ToString(), (int)RarityCode.유니크Ⅲ },
            { RarityCode.유니크Ⅳ.ToString(), (int)RarityCode.유니크Ⅳ },
            { RarityCode.유니크Ⅴ.ToString(), (int)RarityCode.유니크Ⅴ },
            { RarityCode.레전더리Ⅰ.ToString(), (int)RarityCode.레전더리Ⅰ },
            { RarityCode.레전더리Ⅱ.ToString(), (int)RarityCode.레전더리Ⅱ },
            { RarityCode.레전더리Ⅲ.ToString(), (int)RarityCode.레전더리Ⅲ },
            { RarityCode.레전더리Ⅳ.ToString(), (int)RarityCode.레전더리Ⅳ },
            { RarityCode.레전더리Ⅴ.ToString(), (int)RarityCode.레전더리Ⅴ },
            { RarityCode.에픽Ⅰ.ToString(), (int)RarityCode.에픽Ⅰ },
            { RarityCode.에픽Ⅱ.ToString(), (int)RarityCode.에픽Ⅱ },
            { RarityCode.에픽Ⅲ.ToString(), (int)RarityCode.에픽Ⅲ },
            { RarityCode.에픽Ⅳ.ToString(), (int)RarityCode.에픽Ⅳ },
            { RarityCode.에픽Ⅴ.ToString(), (int)RarityCode.에픽Ⅴ },
            { RarityCode.태초.ToString(), (int)RarityCode.태초 }
        };

        public static List<string> FusionSetNames = new List<string>()
        {
            "암영",
            "영혼",
            "황금",
            "용투",
            "정화",
            "행운",
            "돌파",
            "자연",
            "전장",
            "영원",
            "사냥",
            "영역"
        };

        public static List<string> SetItems = new List<string>()
        {
            "영원히 이어지는 황금향 세트",
            "칠흑의 정화 세트",
            "세렌디피티 세트",
            "한계를 넘어선 에너지 세트",
            "소울 페어리 세트",
            "압도적인 자연 세트",
            "고대 전장의 발키리 세트",
            "에테리얼 오브 아츠 세트",
            "그림자에 숨은 죽음 세트",
            "무리 사냥의 길잡이 세트",
            "마력의 영역 세트",
            "용투장의 난 세트"
        };

        public static List<string> SetItemWords = new List<string>()
        {
            "황금향",
            "정화",
            "행운",
            "한계",
            "페어리",
            "자연",
            "발키리",
            "여우",
            "그림자",
            "무리의",
            "마력의",
            "용제", "용왕", "용투", "용의",
            "태초의",
            "영웅담",
            "전설 속의",
            "고유 -"
        };

        private static List<string> _allDungeonNames = new List<string>();

        public static List<string> AllDungeonNames
        {
            get {
                if(_allDungeonNames.Count == 0)
                {
                    _allDungeonNames.AddRange(WeeklyDungeonNames);
                    _allDungeonNames.AddRange(DailyDungeonNames);
                    _allDungeonNames.AddRange(BaseDungeonNames);
                    _allDungeonNames.AddRange(HellDungeonNames);
                }
                return _allDungeonNames; 
            }
        }

        /// <summary>
        /// 중천 아이템 정보. 레거시 제외상태
        /// </summary>
        private static List<Common.Models.DnfApi.ItemInfo> _itemsLv115 = new List<Models.DnfApi.ItemInfo>();
        public static List<Common.Models.DnfApi.ItemInfo> ItemsLv115
        {
            get
            {
                if (_itemsLv115 == null || _itemsLv115.Count == 0)
                {
                    try
                    {
                        string itemDoc = System.IO.File.ReadAllText(@".\ItemDatas\SetItem.json");
                        var temp = JsonConvert.DeserializeObject<List<Common.Models.DnfApi.ItemInfo>>(itemDoc);
                        if(temp != null) { _itemsLv115 = temp; }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("중천 아이템 정보 로드 실패");
                        Console.WriteLine(e);
                    }
                }
                return _itemsLv115;
            }
        }

        public static List<string> RegionDungeonNames = new List<string>()
        {
            "찬사의 광장"
        };

        public static List<string> WeeklyDungeonNames = new List<string>()
        {
            "죽음의 여신전",
            "침묵의 성소",
            "애쥬어 메인",
            "달이 잠긴 호수",
            "꿈결 속 솔리다리스",
            "꿈결 속 흰 구름 계곡",
            "미의 여신 베누스"
        };

        public static List<string> DailyDungeonNames = new List<string>()
        {
            "모독 : 적막의 회랑",
            "모독 : 일렁이는 군도",
            "광포 : 크루얼 비스트",
            "광포 : 청해의 심장",
            "환란 : 별내림 숲",
            "환란 : 길잡이 강"
        };

        public static List<string> BaseDungeonNames = new List<string>()
        {
            "일렁이는 군도",
            "적막의 회랑",
            "크루얼 비스트",
            "청해의 심장",
            "별내림 숲",
            "길잡이 강"
        };

        public static List<string> HellDungeonNames = new List<string>()
        {
            "종말의 숭배자",
            "심연 : 종말의 숭배자"
        };

        public static Dictionary<string, int> SlotOrder = new Dictionary<string, int>()
        {
            { "무기", 0 },
            { "칭호", 1 },
            { "머리어깨", 2 },
            { "상의", 3 },
            { "하의", 4 },
            { "벨트", 5 },
            { "신발", 6 },
            { "팔찌", 7 },
            { "목걸이", 8 },
            { "보조장비", 9 },
            { "반지", 10 },
            { "귀걸이", 11 },
            { "마법석", 12 }
        };

    }

    public static class ExtensionMethod
    {
        public static string UrlEncoding(this string text)
        {
            return System.Web.HttpUtility.UrlEncode(text);
        }
    }

    public enum RarityCode
    {
        유니크Ⅰ = 1200,
        유니크Ⅱ = 1285,
        유니크Ⅲ = 1370,
        유니크Ⅳ = 1455,
        유니크Ⅴ = 1540,
        레전더리Ⅰ = 1650,
        레전더리Ⅱ = 1735,
        레전더리Ⅲ = 1820,
        레전더리Ⅳ = 1905,
        레전더리Ⅴ = 1990,
        에픽Ⅰ = 2100,
        에픽Ⅱ = 2185,
        에픽Ⅲ = 2270,
        에픽Ⅳ = 2355,
        에픽Ⅴ = 2440,
        태초 = 2550
    }
}
