using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    /// <summary>
    /// 서약 코드 정보
    /// </summary>
    public class PledgeCodeHelper
    {
        // 천해천 시즌 시작일 (2025-01-09 06:00)
        public static readonly DateTime Season11 = new DateTime(2026, 3, 26, 6, 0, 0);

        public static string GetSetName(string itemName)
        {
            /*
그림자 :
페어리 :
황금 :
용투 :
정화 :
행운 :
한계 :
자연 :
발키리 :
여우 :
무리 :
마력 :
             */
            if (string.IsNullOrWhiteSpace(itemName)) return string.Empty;

            if (itemName.IndexOf("그림자 :") > -1 || (itemName.IndexOf("그림자 서약") > -1)) 
            {
                return "그림자";
            }
            else if (itemName.IndexOf("페어리 :") > -1 || (itemName.IndexOf("페어리 서약") > -1))
            {
                return "페어리";
            }
            else if (itemName.IndexOf("황금 :") > -1 || (itemName.IndexOf("황금 서약") > -1))
            {
                return "황금";
            }
            else if (itemName.IndexOf("용투 :") > -1 || (itemName.IndexOf("용투 서약") > -1))
            {
                return "용투";
            }
            else if (itemName.IndexOf("정화 :") > -1 || (itemName.IndexOf("정화 서약") > -1))
            {
                return "정화";
            }
            else if (itemName.IndexOf("행운 :") > -1 || (itemName.IndexOf("행운 서약") > -1))
            {
                return "행운";
            }
            else if (itemName.IndexOf("한계 :") > -1 || (itemName.IndexOf("한계 서약") > -1))
            {
                return "한계";
            }
            else if (itemName.IndexOf("자연 :") > -1 || (itemName.IndexOf("자연 서약") > -1))
            {
                return "자연";
            }
            else if (itemName.IndexOf("발키리 :") > -1 || (itemName.IndexOf("발키리 서약") > -1))
            {
                return "발키리";
            }
            else if (itemName.IndexOf("여우 :") > -1 || (itemName.IndexOf("여우 서약") > -1))
            {
                return "여우";
            }
            else if (itemName.IndexOf("무리 :") > -1 || (itemName.IndexOf("무리 서약") > -1))
            {
                return "무리";
            }
            else if (itemName.IndexOf("마력 :") > -1 || (itemName.IndexOf("마력 서약") > -1))
            {
                return "마력";
            }
            else if (itemName.IndexOf("안개 결정") > -1)
            {
                return "고유";
            }

            return string.Empty;
        }

        public static string GetRarity(string itemName)
        {
            /*
             선명한 - 레전더리
완전한 - 에픽
태초의 - 태초
             */
            if (string.IsNullOrWhiteSpace(itemName)) return string.Empty;

            if (itemName.IndexOf("선명한") > -1)
            {
                return "레전더리";
            }
            else if (itemName.IndexOf("완전한") > -1)
            {
                return "에픽";
            }
            else if (itemName.IndexOf("태초의") > -1)
            {
                return "태초";
            }
            else if (itemName.IndexOf("안개 결정") > -1)
            {
                return "레전더리";
            }

            return GetPledgeRarity(itemName);
        }

        // 서약 인경우 레어리티 체크
        private static string GetPledgeRarity(string itemName)
        {
            List<string> begPledge = new List<string>()
            {
                "세계를 태우는 용투 서약",
                "태초로 인도하는 페어리 서약",
                "강림한 여우 서약",
                "현실이 된 이상 속 황금 서약",
                "천란한 신념의 정화 서약",

                "태초에서 현신한 발키리 서약",
                "영원불변의 행운 서약",
                "근원에 닿은 자연 서약",
                "태동하는 울림의 무리 서약",
                "태초에 고동치는 마력 서약",

                "초월하는 한계 서약",
                "태초의 어둠 속 그림자 서약"
            };
            bool existBeg = begPledge.Exists(x => x.Equals(itemName));
            if (existBeg) { return "태초"; }

            List<string> epiPledge = new List<string>()
            {
                "하늘을 태우는 용투 서약",
                "안내하는 페어리 서약",
                "빙의된 여우 서약",
                "손에 잡힌 이상 속 황금 서약",
                "뚜렷한 신념의 정화 서약",

                "결연함이 깃든 발키리 서약",
                "필연성의 행운 서약",
                "만개하는 자연 서약",
                "영원한 맹약의 무리 서약",
                "태동하는 마력 서약,",

                "극복하는 한계 서약",
                "짙은 어둠 속 그림자 서약"
            };
            bool existEpi = epiPledge.Exists(x => x.Equals(itemName));
            if (existEpi) { return "에픽"; }

            List<string> legPledge = new List<string>()
            {
                "대지를 태우는 용투 서약",
                "손짓하는 페어리 서약",
                "부름에 이끌린 여우 서약",
                "목표하는 이상 속 황금 서약",
                "굳건한 신념의 정화 서약",

                "용기가 깃든 발키리 서약",
                "각인된 행운 서약",
                "피어나는 자연 서약",
                "순수한 약속의 무리 서약",
                "맥동하는 마력 서약",

                "돌파하는 한계 서약",
                "흐르는 어둠 속 그림자 서약"
            };
            bool existLeg = legPledge.Exists(x => x.Equals(itemName));
            if (existLeg) { return "레전더리"; }


            return string.Empty;
        }
    }
}
