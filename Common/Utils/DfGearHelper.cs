using Common.Models.DfGear;
using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public class DfGearHelper : HttpClientHelper
    {
        public DfGearHelper(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<List<ItemDetail>> GetTimeLineItems(string charName, string serverName)
        {
            // https://api.dfgear.xyz/character/v2/Timeline?sId=diregie&cName=.6..........&endDate=20250201T0025&cId=
            string url = $"character/v2/Timeline?sId={CodeHelper.GetServerId(serverName)}&cName={charName}";

            _client.DefaultRequestHeaders.Add("gear", "dfgear");
            // GET 요청 보내기
            HttpResponseMessage response = await _client.GetAsync(url);


            // 응답 본문을 문자열로 읽기
            string responseBody = await response.Content.ReadAsStringAsync();

            // 결과 출력
            Console.WriteLine("응답 상태 코드: " + response.StatusCode);
            Console.WriteLine("응답 본문:\n" + responseBody);

            var result = JsonConvert.DeserializeObject<TimeLineResult>(responseBody);

            List<ItemDetail> retValue = new List<ItemDetail>();
            try
            {
               
                foreach (var timeLine in result.TimeLineInfos)
                {
                    var temp = timeLine.Item;
                    temp.ItemId = timeLine.ItemId;
                    temp.Channel = timeLine.Channel;
                    temp.Date = timeLine.Date;
                    //Console.WriteLine($"{timeLine.Item.ItemName} / {timeLine.Item.SetItemName} / {timeLine.Item.ItemRarity} / {timeLine.Item.ItemType}");
                    retValue.Add(temp);
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
                //return new List<ItemDetail>();
                throw new Exception("던파기어 Item 타임라인 조회 실패");
            }

            return retValue;
        }

    }
}
