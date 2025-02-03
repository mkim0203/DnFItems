using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DnFItems.Utils
{
    public class HttpClientHelper
    {
        protected HttpClient _client;

        public HttpClientHelper(string baseUrl)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseUrl);
        }
    }
}
