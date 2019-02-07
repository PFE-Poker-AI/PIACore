using System;
using System.Collections.Generic;

namespace PIACore.Model
{
    /// <summary>
    /// This model represent a poker table.
    /// </summary>
    public class Table
    {
        private List<Card> _cards;
        private int _gamesOccured;
        private Dictionary<string, Player> _players;
        private int _pot;
        private int _smallBlindValue;

        
        /// <summary>
        /// The shown cards on the table.
        /// </summary>
        public int GamesOccured
        {
            get => _gamesOccured;
            set => _gamesOccured = value;
        }
        
        /// <summary>
        /// The shown cards on the table.
        /// </summary>
        public List<Card> Cards
        {
            get => _cards;
            set => _cards = value;
        }
        
        /// <summary>
        /// The current seated players on the table (include players that are out of the current game).
        /// </summary>
        public Dictionary<string, Player> Players
        {
            get => _players;
            set => _players = value;
        }
        
        /// <summary>
        /// The current Pot of the table.
        /// </summary>
        public int Pot
        {
            get => _pot;
            set => _pot = value;
        }
        
        /// <summary>
        /// The current small blind value of the table.
        /// </summary>
        public int SmallBlindValue
        {
            get => _smallBlindValue;
            set => _smallBlindValue = value;
        }

        
        /// <summary>
        /// Instantiates a Table from a given json data.
        /// Use the player list to fill the current
        /// </summary>
        /// <param name="jsonTable">The json object to parse.</param>
        /// <param name="playerName">The AI player name.</param>
        /// <param name="players">The list of players on this table.</param>
        /// <returns>An instantiated Table.</returns>
        public static Table CreateFromJson(Dictionary<string, object> jsonTable, string playerName, Dictionary<string, Player> players)
        {
            List<Card> cards = null;
            if (jsonTable.ContainsKey("visibleCards"))
            {
                cards = Card.CreateFromJsonList((List<object>)jsonTable["visibleCards"]);
            }

            var pot = Convert.ToInt32(jsonTable["superPot"]);
            var blind = Convert.ToInt32(jsonTable["blind"]);
            
            var gamesOccured = Convert.ToInt32(jsonTable["game"]);
            
            var table = new Table
            {
                Cards = cards, Players = players, Pot = pot, SmallBlindValue = blind, _gamesOccured = gamesOccured
            };

            return table;
        }
    }
}