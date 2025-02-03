
using DnFItems.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace DnFItems
{
    [TestClass]
    public class UnitTest1
    {
        public string _dfGearUrl = "https://api.dfgear.xyz";
        [TestMethod]
        public async Task TestMethod1()
        {
            DfGearHelper gear = new DfGearHelper(_dfGearUrl);
            await gear.Test();
        }

        [TestMethod]
        public async Task TestMethod2()
        {
            DfGearHelper gear = new DfGearHelper(_dfGearUrl);
            await gear.Test2();
        }

        [TestMethod]
        public async Task TestMethod3()
        {
            DfGearHelper gear = new DfGearHelper(_dfGearUrl);
            var charResult = await gear.Test2();

            await new DfGearHelper(_dfGearUrl).Test3(charResult.ServerId, charResult.CharacterId);
        }

    }
}
