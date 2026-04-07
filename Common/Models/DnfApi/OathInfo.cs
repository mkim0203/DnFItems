using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.DnfApi
{
    public class OathInfoResult : CharInfo
    {
        [JsonProperty("adventureName")]
        public string AdventureName { get; set; }
        [JsonProperty("guildId")]
        public string GuildId { get; set; }
        [JsonProperty("guildName")]
        public string GuildName { get; set; }

        public OathInfo Oath { get; set; }

    }

    public class OathInfo
    {
        public OathPledgeInfo Info { get; set; }
        public List<OathCrystalInfo> Crystal { get; set; }
        public OathSetInfo SetInfo { get; set; }
    }



    public class OathPledgeInfo
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemRarity { get; set; }
        public int SetPoint { get; set; }
    }

    public class OathCrystalInfo
    {
        public int SlotNo { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemRarity { get; set; }

        public CrystalTune Tune { get; set; }

    }

    public class CrystalTune
    {
        public int Level { get; set; }
        public int SetPoint { get; set; }
    }

    public class OathSetInfo
    {
        public int SetId { get; set; }
        public string SetName { get; set; }
        public string SetOptionName { get; set; }
        public string SetRarityName { get; set; }

        public int SetPoint
        {
            get
            {
                return Active?.SetPoint?.Current ?? 0;
            }
        }

        public OathSetActive Active { get; set; }
        public class OathSetActive
        {
            public OathSetPoint SetPoint { get; set; }
            public class OathSetPoint
            {
                public int Current { get; set; }
            }
        }
    }
}
