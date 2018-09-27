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
    /// <summary>
    /// The connector between the Poker API and the AI.
    /// </summary>
    public class ApiConnector
    {
        private string _tableIdentifier;
        private static readonly HttpClient Client = new HttpClient();
        private static readonly string Url = Environment.GetEnvironmentVariable("REMOTE_API_URL");

        public ApiConnector(string apiKey, string tableIdentifier)
        {
            this._tableIdentifier = tableIdentifier;
            Client.DefaultRequestHeaders.Add("Authorization", apiKey);
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Calls the /hello route, retrieving the player name.
        /// </summary>
        /// <returns></returns>
        public string GetId()
        {
            var response = JsonParser.FromJson(Client.GetStringAsync(Url + "/hello").Result);
            return (string)response["user"];
        }

        /// <summary>
        /// Get all the available table ids.
        /// </summary>
        /// <returns></returns>
        public List<string> GetNewTableIds()
        {
            var tables = new List<string>();
            var response = JsonParser.FromJson(Client.GetStringAsync(Url + "/open-tables").Result);

            foreach (var element in (List<object>)response["tables"])
            {
                var table = (Dictionary<string, object>)element;

                string tableId = (string)table["_id"];
                string tableName = (string)table["id"];
                int seats = Convert.ToInt32(table["seats"]);

                if (tableName.Contains(_tableIdentifier))
                {
                    tables.Add(tableId);
                }
            }

            return tables;
        }

        /// <summary>
        /// Join the given table.
        /// </summary>
        /// <param name="tableId">The table id of the table to join.</param>
        public void JoinGivenTable(string tableId)
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

            HttpResponseMessage response = Client.PostAsync(Url + "/seat", stringContent).Result;
            string responseAsString = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine(responseAsString);
        }

        /// <summary>
        /// The current tables.
        /// </summary>
        /// <returns>A list of all the tables the AI is seated on.</returns>
        public List<string> CurrentTables()
        {
            var tables = new List<string>();
            var response = JsonParser.FromJson(Client.GetStringAsync(Url + "/seating-tables").Result);

            foreach (var element in (List<object>)response["tables"])
            {
                var tableId = (string)element;
                tables.Add(tableId);
            }

            return tables;
        }

        /// <summary>
        /// Get the state (model representation) of a specific table.
        /// </summary>
        /// <param name="tableId">the table id.</param>
        /// <param name="playerName">The player name.</param>
        /// <returns></returns>
        public Table GetTableState(string tableId, string playerName)
        {
            var response = JsonParser.FromJson(Client.GetStringAsync(Url + "/state/" + tableId).Result);

            // Handle the table
            var table = (Dictionary<string, object>)response["table"];

            if (!(bool)table["ready"])
            {
                return null;
            }

            if (table.ContainsKey("currentPlayer") && (string)table["currentPlayer"] != playerName)
            {
                return null;
            }
            var players = Player.CreateAllFromJsonList((List<object>)response["players"], playerName);
            var turnTable = Table.CreateFromJson((Dictionary<string, object>)response["table"], playerName, players);
            return turnTable;
        }

        /// <summary>
        /// Instruct Poker AI to play the given turn.
        /// </summary>
        /// <param name="turn"></param>
        /// <param name="tableId"></param>
        /// <param name="value"></param>
        public void PlayTurn(PlayType turn, string tableId, int value = 0)
        {
            string action;
            var data = new Dictionary<string, object>
            {
                {
                    "tableid", tableId
                }
            };

            switch (turn)
            {
                case PlayType.Call:
                    action = "/call";
                    break;
                case PlayType.Fold:
                    action = "/raise";
                    break;
                case PlayType.Raise:
                    data.Add("raise", value);
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

            HttpResponseMessage response = Client.PostAsync(Url + action, stringContent).Result;
            string responseAsString = response.Content.ReadAsStringAsync().Result;
        }
    }
}