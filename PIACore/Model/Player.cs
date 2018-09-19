using System;
using System.Collections.Generic;

namespace PIACore.Model
{
    public class Player
    {
        private string name;
        private bool _isSelf;
        private List<Card> _cards;
        private int bank;
        private int bid;
        private PlayerPosition position;

        public string Name
        {
            get => name;
            set => name = value;
        }
        public bool IsSelf
        {
            get => _isSelf;
            set => _isSelf = value;
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
        public PlayerPosition Position
        {
            get => position;
            set => position = value;
        }
    }
}