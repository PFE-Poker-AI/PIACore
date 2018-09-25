using System;
using System.Collections.Generic;
using PIACore.Model.Enums;

namespace PIACore.Model
{
    public class Player
    {
        private string name;
        private List<Card> _cards;
        private int bank;
        private int bid;
        private int position;

        public string Name
        {
            get => name;
            set => name = value;
        }
        public bool IsSelf
        {
            get { return _cards != null && _cards.Count > 0; }
        }
        public List<Card> Cards
        {
            get => _cards;
            set => _cards = value;
        }
        public int Bank
        {
            get => bank;
            set => bank = value;
        }
        public int Bid
        {
            get => bid;
            set => bid = value;
        }
        public int Position
        {
            get => position;
            set => position = value;
        }

        public static Dictionary<string, Player> createAllFromJsonList(List<object> jsonPlayers, string playerName)
        {
            var players = new Dictionary<string, Player>();
            foreach (var element in jsonPlayers)
            {
                var player = (Dictionary<string, object>)element;

                var user = (string)player["user"];
                var bank = Convert.ToInt32(player["bankroll"]);
                var bid = Convert.ToInt32(player["bet"]);
                var position = Convert.ToInt32(player["pos"]);
                List<Card> cards = null;
                if (user.Equals(playerName))
                {
                    cards = Card.createFromJsonList((List<object>)player["cards"]);
                }
                players.Add(user,
                            new Player
                            {
                                Bank = bank,
                                Name = user,
                                Cards = cards,
                                Bid = bid,
                                Position = position
                            }
                );
            }

            return players;
        }
    }
}