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
                case 20:
                    retValue = "uni";
                    break;
                case 60:
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
