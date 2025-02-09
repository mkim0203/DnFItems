using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DnFItems
{
    [TestClass]
    public class DfDunDamHelperTest
    {
        public string _dfDunDamUrl = "https://dundam.xyz";

        [TestMethod]
        public async Task GetCharInfoAsyncTest()
        {
            string userId = "상자속냥이";
            string serverName = "디레지에";
            Common.Utils.DfDunDamHelper dundam = new Common.Utils.DfDunDamHelper(_dfDunDamUrl);

            var result = await dundam.GetCharInfoAsync(userId, serverName);
            Console.WriteLine($"{result.CharacterKey} {result.Damage}");
        }

        [TestMethod]
        public async Task GetCharDetailInfoAsyncTest()
        {
            string userId = "상자속냥이";
            string serverName = "디레지에";
            Common.Utils.DfDunDamHelper dundam = new Common.Utils.DfDunDamHelper(_dfDunDamUrl);

            var userInfo = await dundam.GetCharInfoAsync(userId, serverName);
            var userDetailInfo = await dundam.GetCharDetailInfoAsync(userInfo.CharacterKey, userInfo.ServerId);
            Console.WriteLine(userDetailInfo.Rank);
        }
    }
}
