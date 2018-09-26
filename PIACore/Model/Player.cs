using System;
using System.Collections.Generic;
using PIACore.Model.Enums;

namespace PIACore.Model
{
    /// <summary>
    /// This model represents a Player of a poker game.
    /// </summary>
    public class Player
    {
        private string name;
        private List<Card> cards;
        private int bank;
        private int bid;
        private int position;
        private bool isSelf;

        /// <summary>
        /// The name of the player.
        /// </summary>
        public string Name
        {
            get => name;
            set => name = value;
        }
        
        /// <summary>
        /// Tells if this player is the one controlled by the AI.
        /// </summary>
        public bool IsSelf
        {
            get => isSelf;
            set => isSelf = value;
        }

        /// <summary>
        /// List of the player cards.
        /// </summary>
        public List<Card> Cards
        {
            get => cards;
            set => cards = value;
        }
        
        /// <summary>
        /// The current bank amount of the player.
        /// </summary>
        public int Bank
        {
            get => bank;
            set => bank = value;
        }
        
        /// <summary>
        /// The current bid amount of the player.
        /// </summary>
        public int Bid
        {
            get => bid;
            set => bid = value;
        }
        
        /// <summary>
        /// The current position of the player (0 is the first playing player).
        /// </summary>
        public int Position
        {
            get => position;
            set => position = value;
        }

        /// <summary>
        /// Instantiate a Dictionary of players from a given json data.
        /// Their name is used as the dictionary key.
        /// </summary>
        /// <param name="jsonPlayers">The json object representing players.</param>
        /// <param name="playerName">Own player's name.</param>
        /// <returns></returns>
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
                var isSelf = Convert.ToBoolean(player["isSelf"]);
                
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
                                Position = position,
                                IsSelf = isSelf,
                            }
                );
            }

            return players;
        }
    }
}