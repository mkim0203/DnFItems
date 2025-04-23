using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    /// <summary>
    /// 융합 장비
    /// </summary>
    public class FusionItem
    {
        public int HandAndShoulder { get; set; }
        public int Coat { get; set; }
        public int Pants { get; set; }
        public int Belt { get; set; }
        public int Shoes { get; set; }

        public int Brac { get; set; }
        public int Neck { get; set; }
        public int Sup { get; set; } 
        public int Ring { get; set; }
        public int Earing { get; set; }
        public int Ston { get; set; }

        public int AllPoint
        {
            get
            {
                int sum = HandAndShoulder + Coat + Pants + Belt + Shoes;
                sum += Brac + Neck + Sup + Ring + Earing + Ston;
                return sum;
            }
        }
    }
}
