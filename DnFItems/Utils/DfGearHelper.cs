using Common.Models.DfGear;
using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DnFItems.Utils
{
    public class DfGearHelper : HttpClientHelper
    {
        public DfGearHelper(string baseUrl) : base(baseUrl)
        {
        }

        public async Task Test()
        {
            string url = "all?cName=%25EC%2583%2581%25EC%259E%2590%25EC%2586%258D%25EB%2583%25A5%25EC%259D%25B4";
            // https://api.dfgear.xyz/all?cName=%25EC%2583%2581%25EC%259E%2590%25EC%2586%258D%25EB%2583%25A5%25EC%259D%25B4
            var request = new HttpRequestMessage(HttpMethod.Options, url);
            var result = await _client.SendAsync(request);

            // GET 요청 보내기
            HttpResponseMessage response = await _client.GetAsync(url);

            // 응답 성공 여부 확인
            response.EnsureSuccessStatusCode();

            // 응답 본문을 문자열로 읽기
            string responseBody = await response.Content.ReadAsStringAsync();

            // 결과 출력
            Console.WriteLine("응답 상태 코드: " + response.StatusCode);
            Console.WriteLine("응답 본문:\n" + responseBody);
        }

        public async Task<CharInfo> Test2()
        {
            // gear

            string url = "all?cName=%25EC%2583%2581%25EC%259E%2590%25EC%2586%258D%25EB%2583%25A5%25EC%259D%25B4";

            _client.DefaultRequestHeaders.Add("gear", "dfgear");
            // GET 요청 보내기
            HttpResponseMessage response = await _client.GetAsync(url);


            // 응답 본문을 문자열로 읽기
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<CharResult>(responseBody);
            foreach (var charInfo in result.CharInfos)
            {
                Console.WriteLine(charInfo.CharacterId);
            }

            // 결과 출력
            Console.WriteLine("응답 상태 코드: " + response.StatusCode);
            Console.WriteLine("응답 본문:\n" + responseBody);

            if (result?.CharInfos?.Count == 1)
            {
                return result.CharInfos.First();
            }
            else return null;
        }

        public async Task Test3(string serverId, string cId)
        {

            string timelineUrl = $"character/v2/Timeline?sId={serverId}&cName=%25EC%2583%2581%25EC%259E%2590%25EC%2586%258D%25EB%2583%25A5%25EC%259D%25B4&cId={cId}";

            _client.DefaultRequestHeaders.Add("gear", "dfgear");
            // GET 요청 보내기
            HttpResponseMessage response = await _client.GetAsync(timelineUrl);


            // 응답 본문을 문자열로 읽기
            string responseBody = await response.Content.ReadAsStringAsync();

            // 결과 출력
            Console.WriteLine("응답 상태 코드: " + response.StatusCode);
            Console.WriteLine("응답 본문:\n" + responseBody);

            var result = JsonConvert.DeserializeObject<TimeLineResult>(responseBody);
            foreach (var timeLine in result.TimeLineInfos)
            {
                Console.WriteLine($"{timeLine.Item.ItemName} / {timeLine.Item.SetItemName} / {timeLine.Item.ItemRarity} / {timeLine.Item.ItemType}");
            }

            
        }
    }
}
