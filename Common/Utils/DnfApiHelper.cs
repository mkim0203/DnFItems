
using Common.Models.DnfApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common.Utils
{
    public class DnfApiHelper : HttpClientHelper
    {
        private string apiKey = "4YDj0cxaj53cDXPC4CDnGT7arVoPwprw";

        public DnfApiHelper(string baseUrl) : base(baseUrl)
        {
            // https://api.neople.co.kr
        }

        public async Task<CharInfo> GetCharInfoAsync(string charName, string serverNameOrId, bool isServerId = false)
        {
            
            string url = $"df/servers/{(isServerId ? serverNameOrId : CodeHelper.GetServerId(serverNameOrId))}/characters?characterName={charName.UrlEncoding()}&apikey={apiKey}";
            // GET 요청 보내기
            HttpResponseMessage response = await _client.GetAsync(url);

            try
            {
                // 응답 본문을 문자열로 읽기
                string responseBody = await response.Content.ReadAsStringAsync();

                // 결과 출력
                Console.WriteLine("응답 상태 코드: " + response.StatusCode);
                Console.WriteLine("응답 본문:\n" + responseBody);

                if(response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return null;
                }

                var result = JsonConvert.DeserializeObject<CharInfoResult>(responseBody);

                if (result?.CharInfos?.Count > 0)
                {
                    return result.CharInfos.First();
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<EquipmentResult> GetEquipmentsAsync(string charKey, string serverNameOrId, bool isServerId = false)
        {
            string url = $"df/servers/{(isServerId ? serverNameOrId : CodeHelper.GetServerId(serverNameOrId))}/characters/{charKey}/equip/equipment?apikey={apiKey}";

            // GET 요청 보내기
            HttpResponseMessage response = await _client.GetAsync(url);

            try
            {
                // 응답 본문을 문자열로 읽기
                string responseBody = await response.Content.ReadAsStringAsync();

                // 결과 출력
                Console.WriteLine("응답 상태 코드: " + response.StatusCode);
                Console.WriteLine("응답 본문:\n" + responseBody);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return null;
                }

                var result = JsonConvert.DeserializeObject<EquipmentResult>(responseBody);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }


        public async Task<TimeLineResult> GetTimeLineAsync(string charKey, string serverNameOrId, DateTime stDate, DateTime edDate, bool isServerId = false)
        {
            string url = $"df/servers/{(isServerId ? serverNameOrId : CodeHelper.GetServerId(serverNameOrId))}/characters/{charKey}/timeline?limit=100&startDate={stDate.ToString("yyyy-MM-dd")}&endDate={edDate.ToString("yyyy-MM-dd HH:mm")}&code=504,505,507,511,513,516&apikey={apiKey}";

            // GET 요청 보내기
            HttpResponseMessage response = await _client.GetAsync(url);

            try
            {
                // 응답 본문을 문자열로 읽기
                string responseBody = await response.Content.ReadAsStringAsync();

                // 결과 출력
                Console.WriteLine("응답 상태 코드: " + response.StatusCode);
                Console.WriteLine("응답 본문:\n" + responseBody);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return null;
                }

                var result = JsonConvert.DeserializeObject<TimeLineResult>(responseBody);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        /// <summary>
        /// 타임라인 조회 - 서약
        /// </summary>
        /// <param name="charKey"></param>
        /// <param name="serverNameOrId"></param>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <param name="isServerId"></param>
        /// <returns></returns>
        public async Task<TimeLineResult> GetPledgeTimeLineAsync(string charKey, string serverNameOrId, DateTime stDate, DateTime edDate, bool isServerId = false)
        {
            string url = $"df/servers/{(isServerId ? serverNameOrId : CodeHelper.GetServerId(serverNameOrId))}/characters/{charKey}/timeline?limit=100&startDate={stDate.ToString("yyyy-MM-dd")}&endDate={edDate.ToString("yyyy-MM-dd HH:mm")}&code=507,550,551,552,553,554&apikey={apiKey}";

            // GET 요청 보내기
            HttpResponseMessage response = await _client.GetAsync(url);

            try
            {
                // 응답 본문을 문자열로 읽기
                string responseBody = await response.Content.ReadAsStringAsync();

                // 결과 출력
                Console.WriteLine("응답 상태 코드: " + response.StatusCode);
                Console.WriteLine("응답 본문:\n" + responseBody);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return null;
                }

                var result = JsonConvert.DeserializeObject<TimeLineResult>(responseBody);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        /// <summary>
        /// 타임라인 조회 - 레기온 클리어
        /// </summary>
        /// <param name="charKey"></param>
        /// <param name="serverName"></param>
        /// <param name="stDate"></param>
        /// <param name="edDate"></param>
        /// <returns></returns>
        public async Task<TimeLineResult> GetTimeLineRegionAsync(string charKey, string serverName, DateTime stDate, DateTime edDate)
        {
            string url = $"df/servers/{CodeHelper.GetServerId(serverName)}/characters/{charKey}/timeline?limit=100&startDate={stDate.ToString("yyyy-MM-dd")}&endDate={edDate.ToString("yyyy-MM-dd HH:mm")}&code=209&apikey={apiKey}";

            // GET 요청 보내기
            HttpResponseMessage response = await _client.GetAsync(url);

            try
            {
                // 응답 본문을 문자열로 읽기
                string responseBody = await response.Content.ReadAsStringAsync();

                // 결과 출력
                Console.WriteLine("응답 상태 코드: " + response.StatusCode);
                Console.WriteLine("응답 본문:\n" + responseBody);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return null;
                }

                var result = JsonConvert.DeserializeObject<TimeLineResult>(responseBody);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<TimeLineResult> GetTimeLineRaidAsync(string charKey, string serverName, DateTime stDate, DateTime edDate)
        {
            string url = $"df/servers/{CodeHelper.GetServerId(serverName)}/characters/{charKey}/timeline?limit=100&startDate={stDate.ToString("yyyy-MM-dd")}&endDate={edDate.ToString("yyyy-MM-dd HH:mm")}&code=201,210&apikey={apiKey}";

            // GET 요청 보내기
            HttpResponseMessage response = await _client.GetAsync(url);

            try
            {
                // 응답 본문을 문자열로 읽기
                string responseBody = await response.Content.ReadAsStringAsync();

                // 결과 출력
                Console.WriteLine("응답 상태 코드: " + response.StatusCode);
                Console.WriteLine("응답 본문:\n" + responseBody);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return null;
                }

                var result = JsonConvert.DeserializeObject<TimeLineResult>(responseBody);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<ItemInfoResult> GetItemInfoAsync(string findText, string itemRarity)
        {
            string url = $"df/items?itemName={findText.UrlEncoding()}&wordType=full&q=minLevel:115,maxLevel:115,rarity:{itemRarity.UrlEncoding()}&limit=30&apikey={apiKey}";

            // GET 요청 보내기
            HttpResponseMessage response = await _client.GetAsync(url);

            try
            {
                // 응답 본문을 문자열로 읽기
                string responseBody = await response.Content.ReadAsStringAsync();

                // 결과 출력
                Console.WriteLine("응답 상태 코드: " + response.StatusCode);
                Console.WriteLine("응답 본문:\n" + responseBody);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return null;
                }

                var result = JsonConvert.DeserializeObject<ItemInfoResult>(responseBody);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<OathInfoResult> GetCharOathAsync (string charKey, string serverNameOrId, bool isServerId = false)
        {
            string url = $"df/servers/{(isServerId ? serverNameOrId : CodeHelper.GetServerId(serverNameOrId))}/characters/{charKey}/equip/oath?apikey={apiKey}";
            // GET 요청 보내기
            HttpResponseMessage response = await _client.GetAsync(url);

            try
            {
                // 응답 본문을 문자열로 읽기
                string responseBody = await response.Content.ReadAsStringAsync();

                // 결과 출력
                Console.WriteLine("응답 상태 코드: " + response.StatusCode);
                Console.WriteLine("응답 본문:\n" + responseBody);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return null;
                }

                var result = JsonConvert.DeserializeObject<OathInfoResult>(responseBody);

                return result;
                 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
