using System;
using System.Net.Http;

namespace PIACore.Web
{
    public class ApiConnector
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string url = Environment.GetEnvironmentVariable("REMOTE_API_URL");

        public string getId()
        {
            client.DefaultRequestHeaders.Add("token",Environment.GetEnvironmentVariable("REMOTE_API_TOKEN"));

            return "";
        }
        
    }
}