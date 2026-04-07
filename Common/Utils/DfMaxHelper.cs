using Common.Models.DfMax;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public class DfMaxHelper : HttpClientHelper
    {
        // https://dfmax.xyz/characters?category=guild&name=nugget
        public DfMaxHelper(string baseUrl) : base(baseUrl)
        {

        }

        public async Task<List<CharacterInfo>> GetGuildUsersAsync(string guildName)
        {
            string url = $"characters?category=guild&name={guildName}";

            // GET 요청 보내기
            HttpResponseMessage response = await _client.GetAsync(url);


            // 응답 본문을 문자열로 읽기
            string responseBody = await response.Content.ReadAsStringAsync();

            // 결과 출력
            Console.WriteLine("응답 상태 코드: " + response.StatusCode);
            //Console.WriteLine("응답 본문:\n" + responseBody);
            return ExtractCharacterData(responseBody);

        }

        public async Task<List<CharacterInfo>> GetAdvenUsersAsync(string advenName)
        {
            string url = $"characters?category=adventure&name={advenName}";

            // GET 요청 보내기
            HttpResponseMessage response = await _client.GetAsync(url);


            // 응답 본문을 문자열로 읽기
            string responseBody = await response.Content.ReadAsStringAsync();

            // 결과 출력
            Console.WriteLine("응답 상태 코드: " + response.StatusCode);
            //Console.WriteLine("응답 본문:\n" + responseBody);
            return ExtractCharacterData2(responseBody);

        }

        private List<CharacterInfo> ExtractCharacterData(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var characterList = new List<CharacterInfo>();

            var charObjects = doc.DocumentNode.SelectNodes("//div[contains(@class, 'char-object')]");

            if (charObjects == null)
                return characterList;

            foreach (var charObject in charObjects)
            {
                var anchor = charObject.SelectSingleNode(".//a");
                var nameNode = charObject.SelectSingleNode(".//span[@class='charName']");
                var fameNode = charObject.SelectSingleNode(".//span[contains(@class, 'fame-value')]");
                var damageNode = charObject.SelectSingleNode(".//span[contains(@class, 'damage-score')]");
                var buffNode = charObject.SelectSingleNode(".//span[contains(@class, 'buff-score')]");

                if (anchor != null && nameNode != null && fameNode != null)
                {
                    string href = anchor.GetAttributeValue("href", "");
                    string[] parts = href.Split('/');

                    if (parts.Length >= 4)
                    {
                        string server = parts[2];  // "/character/{서버}/{user key}" 구조
                        string userKey = parts[3];
                        string name = nameNode.InnerText.Trim();
                        int? fame = null;
                        try
                        {
                            if(fameNode == null) fame = null;
                            else fame = Convert.ToInt32(fameNode.InnerText);
                        }
                        catch { fame = null; }

                        long? damege = null;
                        try
                        {
                            if(damageNode == null) damege = null;
                            else damege = Convert.ToInt64(damageNode?.InnerText);
                        }
                        catch { damege = null; }

                        long? buff = null;
                        try
                        {
                            if(buffNode == null) buff = null;
                            else buff = Convert.ToInt64(buffNode?.InnerText);
                        }
                        catch { buff = null; }

                        characterList.Add(new CharacterInfo
                        {
                            ServerId = server,
                            CharacterKey = userKey,
                            Name = name,
                            Fame = fame,
                            Damage = damege,
                            Buff = buff
                        });
                    }
                }
            }

            return characterList;
        }

        private List<CharacterInfo> ExtractCharacterData2(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var characterList = new List<CharacterInfo>();

            var charObjects = doc.DocumentNode.SelectNodes("//div[contains(@class, 'ease-out')]");

            if (charObjects == null)
                return characterList;

            foreach (var charObject in charObjects)
            {
                var anchor = charObject.SelectSingleNode(".//a[contains(@href, '/characters/')]");
                var nameNode = charObject.SelectSingleNode(".//p[contains(@class, 'font-bold')]");
                var fameNode = charObject.SelectSingleNode(".//p[contains(@class, 'adaptive-fame-color')]");
                //var damageNode = charObject.SelectSingleNode(".//p[contains(@class, 'font-semibold')]");
                HtmlNode damageNode = null;
                //var buffNode = charObject.SelectSingleNode(".//span[contains(@class, 'buff-score')]");
                HtmlNode buffNode = null;

                if (anchor != null && nameNode != null && fameNode != null)
                {
                    string href = anchor.GetAttributeValue("href", "");
                    string[] parts = href.Split('/');

                    if (parts.Length >= 4)
                    {
                        string server = parts[2];  // "/character/{서버}/{user key}" 구조
                        string userKey = parts[3];
                        string name = nameNode.InnerText.Trim();
                        int? fame = null;
                        try
                        {
                            if (fameNode == null) fame = null;
                            else fame = Convert.ToInt32(fameNode.InnerText.Replace(",", ""));
                        }
                        catch { fame = null; }

                        long? damege = null;
                        try
                        {
                            if (damageNode == null) damege = null;
                            else damege = Convert.ToInt64(damageNode?.InnerText);
                        }
                        catch { damege = null; }

                        long? buff = null;
                        try
                        {
                            if (buffNode == null) buff = null;
                            else buff = Convert.ToInt64(buffNode?.InnerText);
                        }
                        catch { buff = null; }

                        characterList.Add(new CharacterInfo
                        {
                            ServerId = server,
                            CharacterKey = userKey,
                            Name = name,
                            Fame = fame,
                            Damage = damege,
                            Buff = buff
                        });
                    }
                }
            }

            return characterList;
        }
    }
}
