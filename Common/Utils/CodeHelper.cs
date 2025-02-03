using System;
using System.Collections.Generic;
using System.Linq;
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
            }

            return serverId;
        }

        public static string GetRarityColor(string targetRarity)
        {
            /*
             * 
            .uni {
		        color: hotpink;
	        }
	        .leg {
		        color: darkorange;
	        }
	        .epi {
		        color: goldenrod;
	        }
	        .beg {
		        color: deepskyblue;
	        }

             */
            if (targetRarity == "유니크") return "uni";
            if (targetRarity == "레전더리") return "leg";
            if (targetRarity == "에픽") return "epi";
            if (targetRarity == "태초") return "beg";
            return "";
        }
    }
}
