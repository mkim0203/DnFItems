using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.DfMax
{
    public class CharacterInfo
    {
        public string ServerId { get; set; }
        public string CharacterKey { get; set; }
        public string Name { get; set; }
        public int? Fame { get; set; }

        public long? Damage { get; set; }
        public long? Buff { get; set; }
    }
}
