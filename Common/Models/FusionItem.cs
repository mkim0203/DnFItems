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

        public int AllPoint
        {
            get
            {
                int sum = HandAndShoulder + Coat + Pants + Belt + Shoes;
                return sum;
            }
        }
    }
}
