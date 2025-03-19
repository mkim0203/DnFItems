using Common.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DnFItems
{
    [TestClass]
    public class GithubHelperTest
    {
        [TestMethod]
        public void GetLastVersionTest()
        {
            GithubHelper helper = new GithubHelper("https://raw.githubusercontent.com/");
            var version = helper.GetLastVersionAsync().Result;
            Console.WriteLine(version);

            Version currentVersion = new Version("1.8.5");

            var newVersion = new Version(version);
            if (currentVersion.CompareTo(newVersion) < 0)
            {
                Console.WriteLine("New version is available!");
            }
            else
            {
                Console.WriteLine("You are using the latest version.");
            }
        }
    }
}
