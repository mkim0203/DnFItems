using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    /// <summary>
    /// 서약 요약 정보
    /// </summary>
    public class PledgeSummary
    {
        public PledgeSummary(List<TimeLinePledgeItem> timeLineDatas)
        {
            if (timeLineDatas != null && timeLineDatas.Count > 0)
            {
                SummaryDatas = timeLineDatas;
                MakeData();
            }
        }
        // 타임라인 전체 정보
        private List<TimeLinePledgeItem> SummaryDatas = new List<TimeLinePledgeItem>();

        // 서약 세트별 count 정보
        private List<PledgeSetCountInfo> PledgeSetInfos = new List<PledgeSetCountInfo>();

        public List<PledgeSetCountInfo> SummaryInfo
        {
            get
            {
                return PledgeSetInfos;
            }
        }

        private void MakeData()
        {
            List<string> setNames = new List<string>()
            {
                "그림자",
                "페어리",
                "황금",
                "용투",
                "정화",
                "행운",
                "한계",
                "자연",
                "발키리",
                "여우",
                "무리",
                "마력"
            };
            PledgeSetInfos.Clear();
            foreach (string setName in setNames)
            {
                var findPledge = SummaryDatas
                    .Where(x => x.SetItemName == setName && x.IsPledge)
                    .OrderByDescending(x => x.ItemRarityLevel)
                    .FirstOrDefault();
                
                PledgeSetCountInfo info = new PledgeSetCountInfo()
                {
                    Name = setName,
                    PledgeRarity = findPledge == null ? string.Empty : findPledge.ItemRarity,
                    PledgeRarityLevel = findPledge == null ? 0 : findPledge.ItemRarityLevel,
                    BegCount = SummaryDatas.Count(x => x.SetItemName == setName && x.IsPledge == false && x.ItemRarity == "태초"),
                    EpiCount = SummaryDatas.Count(x => x.SetItemName == setName && x.IsPledge == false && x.ItemRarity == "에픽"),
                    LegCount = SummaryDatas.Count(x => x.SetItemName == setName && x.IsPledge == false && x.ItemRarity == "레전더리"),
                    Session10LegCount = SummaryDatas.Count(x => x.SetItemName == "고유" && x.IsPledge == false && x.ItemRarity == "레전더리")
                };
                PledgeSetInfos.Add(info);
            }

        }
    }

    public class PledgeSetCountInfo
    {
        public string Name { get; set; }
        public string PledgeRarity { get; set; }
        public int PledgeRarityLevel { get; set; }
        // 태초 count
        public int BegCount { get; set; }
        // 에픽 count
        public int EpiCount { get; set; }
        // 레전더리 count
        public int LegCount { get; set; }
        // 시즌10 고유 레전더리 서약 count
        public int Session10LegCount { get; set; }

    }
}
