using Common.Models.DfMax;
using Common.Utils;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnFItems
{
    [TestClass]
    public class DfmaxHelperTest
    {
        [TestMethod]
        public async Task GetGuildUsersTest()
        {
            DfMaxHelper helper = new DfMaxHelper("https://dfmax.xyz");
            var result = await helper.GetGuildUsersAsync("nugget");
             
            // 결과 출력
            foreach (var character in result)
            {
                Console.WriteLine($"서버: {character.ServerId}, 유저 키: {character.CharacterKey}, 이름: {character.Name}, 명성: {character.Fame}");
            }
        }

        [TestMethod]
        public async Task GetAdvenUsersTest()
        {
            DfMaxHelper helper = new DfMaxHelper("https://dfmax.xyz");
            var result = await helper.GetAdvenUsersAsync("이건머임s");

            // 결과 출력
            foreach (var character in result)
            {
                Console.WriteLine($"서버: {character.ServerId}, 유저 키: {character.CharacterKey}, 이름: {character.Name}, 명성: {character.Fame}");
            }
        }

    }

    

}
