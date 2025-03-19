using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public class GithubHelper : HttpClientHelper
    {
        public GithubHelper(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<string> GetLastVersionAsync()
        {
            // https://raw.githubusercontent.com/mkim0203/DnFItems/refs/heads/master/MySetItem/VersionCheck.txt
            string url = "mkim0203/DnFItems/refs/heads/master/MySetItem/VersionCheck.txt";

            // GET 요청 보내기
            HttpResponseMessage response = await _client.GetAsync(url);

            // 응답 본문을 문자열로 읽기
            string responseBody = await response.Content.ReadAsStringAsync();

            // 결과 출력
            Console.WriteLine("응답 상태 코드: " + response.StatusCode);
            Console.WriteLine("응답 본문:\n" + responseBody);

            return responseBody;
        }
    }
}
