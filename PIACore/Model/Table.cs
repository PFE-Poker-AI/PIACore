using System;
using System.Collections.Generic;

namespace PIACore.Model
{
    public class Table
    {
        private List<Card> _cards;
        private Dictionary<string, Player> _players;
        private int _pot;
        private int _smallBlindValue;

        public List<Card> Cards
        {
            get => _cards;
            set => _cards = value;
        }
        public Dictionary<string, Player> Players
        {
            get => _players;
            set => _players = value;
        }
        public int Pot
        {
            get => _pot;
            set => _pot = value;
        }
        public int SmallBlindValue
        {
            get => _smallBlindValue;
            set => _smallBlindValue = value;
        }

        public static Table createFromJson(Dictionary<string, object> jsonTable, string playerName, Dictionary<string, Player> players)
        {
            List<Card> cards = null;
            if (jsonTable.ContainsKey("visibleCards"))
            {
                cards = Card.createFromJsonList((List<object>)jsonTable["visibleCards"]);
            }

            var pot = Convert.ToInt32(jsonTable["pot"]);
            var blind = Convert.ToInt32(jsonTable["blind"]);

            var table = new Table
            {
                Cards = cards, Players = players, Pot = pot, SmallBlindValue = blind
            };

            return table;
        }
    }
}