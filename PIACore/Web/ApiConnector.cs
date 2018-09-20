using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Json;
using Newtonsoft.Json;
using PIACore.Model;
using PIACore.Model.Enums;

namespace PIACore.Web
{
    public class ApiConnector
    {
        private static readonly HttpClient Client = new HttpClient();
        private static readonly string url = Environment.GetEnvironmentVariable("REMOTE_API_URL");


        public ApiConnector()
        {
            Client.DefaultRequestHeaders.Add("Authorization", Environment.GetEnvironmentVariable("REMOTE_API_KEY"));
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
        }
        public string getId()
        {
            
            var response = JsonParser.FromJson(Client.GetStringAsync(url + "/hello").Result);
            return (string) response["user"];
        }

        public List<string> getNewTableIds()
        {
            var tables = new List<string>();
            var response = JsonParser.FromJson(Client.GetStringAsync(url + "/open-tables").Result);

            foreach (var element in (List<object>)response["tables"])
            {
                var table = (Dictionary<string, object>) element;
                
                string tableId = (string)table["_id"];
                string tableName = (string)table["id"];
                int seats = Convert.ToInt32(table["seats"]);

                if (tableName.Contains(Environment.GetEnvironmentVariable("AI_TABLE_IDENTIFIER")))
                {
                    tables.Add(tableId);
                }
            }

            return tables;
        }

        public void joinGivenTable(string tableId)
        {   
            
            var value = JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {
                    "tableid", tableId
                }
            }, Formatting.Indented);
            
            StringContent stringContent = new StringContent(
                value,
                Encoding.UTF8,
                "application/json");
            
            HttpResponseMessage response = Client.PostAsync(url + "/seat", stringContent).Result;
            string responseAsString =  response.Content.ReadAsStringAsync().Result;

            Console.WriteLine(responseAsString);
        }

        public List<string> currentTables()
        {

            var tables = new List<string>();
            var response = JsonParser.FromJson(Client.GetStringAsync(url + "/seating-tables").Result);

            foreach (var element in (List<object>)response["tables"])
            {
                var tableId = (string) element;
                tables.Add(tableId);
            }

            return tables;
        }

        public Table getTableState(string tableId)
        {
            var response = JsonParser.FromJson(Client.GetStringAsync(url + "/state/"+tableId).Result);
            
            // Handle the players
            foreach (var element in (List<object>)response["players"])
            {
                var playerElement = (Dictionary<string, object>) element;
            }

            
            // Handle the table
            foreach (var element in (List<object>)response["table"])
            {
                var tableElement = (Dictionary<string, object>) element;
            }
            
            
            
            return null;
        }

        public void playTurn(PlayType turn,string playerId, int value = 0)
        {
            
            string action;
            var data = new Dictionary<string, object>
            {
                {
                    "playerid", playerId
                }
            };

            switch (turn)
            {
                case PlayType.call :
                    action = "/call";
                    break;
                case PlayType.fold :
                    action = "/raise";
                    break;
                case PlayType.raise :
                    data.Add("raise",value);
                    action = "/fold";
                    break;
                default:
                    action = ""; 
                    break;
            }
            
            var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            
            StringContent stringContent = new StringContent(
                jsonData,
                Encoding.UTF8,
                "application/json");
            
            HttpResponseMessage response = Client.PostAsync(url + action, stringContent).Result;
            string responseAsString =  response.Content.ReadAsStringAsync().Result;
        }
    }
}