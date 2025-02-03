using Common.Models.DfDunDam;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common.Utils
{
    public class DfDunDamHelper : HttpClientHelper
    {
        public DfDunDamHelper(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<CharInfo> GetCharInfoAsync(string userId, string serverName)
        {
            string url = $"dat/searchData.jsp?server={CodeHelper.GetServerId(serverName)}&name={System.Web.HttpUtility.UrlEncode(userId)}";

            // User-Agent 헤더 추가. 없으면 던담에서 오류남
            _client.DefaultRequestHeaders.Add("User-Agent", "mySetItem");


            var jsonBody = "{}";
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            //var content = new StringContent(jsonBody);
            HttpResponseMessage response = await _client.PostAsync(url, content);


            // 응답 본문을 문자열로 읽기
            string responseBody = await response.Content.ReadAsStringAsync();

            // 결과 출력
            Console.WriteLine("응답 상태 코드: " + response.StatusCode);
            Console.WriteLine("응답 본문:\n" + responseBody);

            try
            {

                var result = JsonConvert.DeserializeObject<CharResult>(responseBody);
                if (result?.CharInfos?.Count > 0) { return result.CharInfos.First(); }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //return null;
                throw new Exception("던담 캐릭터 기본정보 조회 실패");
            }

            return null;
        }

        public async Task<CharDetailInfo> GetCharDetailInfoAsync(string userKey, string serverId)
        {
            string detailUrl = $"dat/viewData.jsp?image={userKey}&server={serverId}";

            // User-Agent 헤더 추가. 없으면 던담에서 오류남
            _client.DefaultRequestHeaders.Add("User-Agent", "mySetItem");

            var jsonBody = "{}";
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            //var content = new StringContent(jsonBody);
            HttpResponseMessage response = await _client.PostAsync(detailUrl, content);


            // 응답 본문을 문자열로 읽기
            string responseBody = await response.Content.ReadAsStringAsync();

            // 결과 출력
            Console.WriteLine("응답 상태 코드: " + response.StatusCode);
            Console.WriteLine("응답 본문:\n" + responseBody);

            try
            {
                var result = JsonConvert.DeserializeObject<CharDetailInfo>(responseBody);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //return null;
                throw new Exception("던담 캐릭터 상세정보 조회 실패");
            }
        }
    }
}
