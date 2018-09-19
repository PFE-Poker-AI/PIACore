using System.Collections.Generic;

namespace PIACore.Model
{
    public class Table
    {
        private string _id;
        private Card[] _cards;
        private Dictionary<string, Player> _players;

        public Card[] Cards
        {
            get => _cards;
        }
        public Dictionary<string, Player> Players
        {
            get => _players;
        }
        
        

        public Table(string id, Card[] cards, Dictionary<string, Player> players)
        {
            _id = id;
            _cards = cards;
            _players = players;
        }
    }
}