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
        private string _name;
        private List<Card> _cards;
        private int _bank;
        private int _bid;
        private int _position;
        private bool _isSelf;

        /// <summary>
        /// The name of the player.
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        /// <summary>
        /// Tells if this player is the one controlled by the AI.
        /// </summary>
        public bool IsSelf
        {
            get => _isSelf;
            set => _isSelf = value;
        }

        /// <summary>
        /// List of the player cards.
        /// </summary>
        public List<Card> Cards
        {
            get => _cards;
            set => _cards = value;
        }
        
        /// <summary>
        /// The current bank amount of the player.
        /// </summary>
        public int Bank
        {
            get => _bank;
            set => _bank = value;
        }
        
        /// <summary>
        /// The current bid amount of the player.
        /// </summary>
        public int Bid
        {
            get => _bid;
            set => _bid = value;
        }
        
        /// <summary>
        /// The current position of the player (0 is the first playing player).
        /// </summary>
        public int Position
        {
            get => _position;
            set => _position = value;
        }

        /// <summary>
        /// Instantiate a Dictionary of players from a given json data.
        /// Their name is used as the dictionary key.
        /// </summary>
        /// <param name="jsonPlayers">The json object representing players.</param>
        /// <param name="playerName">Own player's name.</param>
        /// <returns></returns>
        public static Dictionary<string, Player> CreateAllFromJsonList(List<object> jsonPlayers, string playerName)
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
                    cards = Card.CreateFromJsonList((List<object>)player["cards"]);
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