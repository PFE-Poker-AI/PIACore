using System;
using System.Net.Http;
using Json;
using PIACore.Model;

namespace PIACore.Web
{
    public class ApiConnector
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string url = Environment.GetEnvironmentVariable("REMOTE_API_URL");

        public string getId()
        {
            client.DefaultRequestHeaders.Add("token", Environment.GetEnvironmentVariable("REMOTE_API_KEY"));
            var response = JsonParser.FromJson(client.GetStringAsync(url + "/hello").Result);
            return (string) response["username"];
        }

        public Table getTable()
        {
            return null;
        }
    }
}