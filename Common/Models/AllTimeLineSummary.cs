using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    /// <summary>
    /// 캐릭터 아이템 timeline 전체 정보
    /// </summary>
    public class AllTimeLineSummary
    {
        public List<TimeLineItem> Datas = new List<TimeLineItem>();

        public AllTimeLineSummary(List<TimeLineItem> allTimeLine)
        {
            if(allTimeLine != null) Datas.AddRange(allTimeLine);
        }

        public IEnumerable<TimeLineItem> AllBeg { get { return Datas.Where(x => x.ItemRarity.Equals("태초")); } }
        public IEnumerable<TimeLineItem> AllEpi { get { return Datas.Where(x => x.ItemRarity.Equals("에픽")); } }
        public IEnumerable<TimeLineItem> AllLeg { get { return Datas.Where(x => x.ItemRarity.Equals("레전더리")); } }


        public IEnumerable<TimeLineItem> AllBaseHell { get { return Datas.Where(x => x.IsBaseHell); } }
        public IEnumerable<TimeLineItem> AllBaseHellBeg { get { return AllBaseHell.Where(x => x.ItemRarity.Equals("태초")); } }
        public IEnumerable<TimeLineItem> AllBaseHellEpi { get { return AllBaseHell.Where(x => x.ItemRarity.Equals("에픽")); } }
        public IEnumerable<TimeLineItem> AllBaseHellLeg { get { return AllBaseHell.Where(x => x.ItemRarity.Equals("레전더리")); } }

        public IEnumerable<TimeLineItem> AllSpHell { get { return Datas.Where(x => x.IsSpHell); } }
        public IEnumerable<TimeLineItem> AllSpHellBeg { get { return AllSpHell.Where(x => x.ItemRarity.Equals("태초")); } }
        public IEnumerable<TimeLineItem> AllSphellEpi { get { return AllSpHell.Where(x => x.ItemRarity.Equals("에픽")); } }
        public IEnumerable<TimeLineItem> AllSphellLeg { get { return AllSpHell.Where(x => x.ItemRarity.Equals("레전더리")); } }

        public IEnumerable<TimeLineItem> AllWeekly { get { return Datas.Where(x => x.IsWeekly); } }
        public IEnumerable<TimeLineItem> AllWeeklyBeg { get { return AllWeekly.Where(x => x.ItemRarity.Equals("태초")); } }
        public IEnumerable<TimeLineItem> AllWeeklyEpi { get { return AllWeekly.Where(x => x.ItemRarity.Equals("에픽")); } }
        public IEnumerable<TimeLineItem> AllWeeklyLeg { get { return AllWeekly.Where(x => x.ItemRarity.Equals("레전더리")); } }

        public IEnumerable<TimeLineItem> AllRegion { get { return Datas.Where(x => x.IsRegion); } }
        public IEnumerable<TimeLineItem> AllRegionBeg { get { return AllRegion.Where(x => x.ItemRarity.Equals("태초")); } }
        public IEnumerable<TimeLineItem> AllRegionEpi { get { return AllRegion.Where(x => x.ItemRarity.Equals("에픽")); } }
        public IEnumerable<TimeLineItem> AllRegionLeg { get { return AllRegion.Where(x => x.ItemRarity.Equals("레전더리")); } }

        public IEnumerable<TimeLineItem> AllDaily { get { return Datas.Where(x => x.IsDaily); } }
        public IEnumerable<TimeLineItem> AllDailyBeg { get { return AllDaily.Where(x => x.ItemRarity.Equals("태초")); } }
        public IEnumerable<TimeLineItem> AllDailyEpi { get { return AllDaily.Where(x => x.ItemRarity.Equals("에픽")); } }
        public IEnumerable<TimeLineItem> AllDailyLeg { get { return AllDaily.Where(x => x.ItemRarity.Equals("레전더리")); } }

        public IEnumerable<TimeLineItem> AllBaseDungeon { get { return Datas.Where(x => x.IsBaseDungeon); } }
        public IEnumerable<TimeLineItem> AllBaseDungeonBeg { get { return AllBaseDungeon.Where(x => x.ItemRarity.Equals("태초")); } }
        public IEnumerable<TimeLineItem> AllBaseDungeonEpi { get { return AllBaseDungeon.Where(x => x.ItemRarity.Equals("에픽")); } }
        public IEnumerable<TimeLineItem> AllBaseDungeonLeg { get { return AllBaseDungeon.Where(x => x.ItemRarity.Equals("레전더리")); } }
    }
}
