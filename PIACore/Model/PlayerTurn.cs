namespace PIACore.Model
{
    public class PlayerTurn
    {
        private bool _isOut;
        private int _bank;
        private int _bet;
        private int _position;
        
        public bool IsOut
        {
            get => _isOut;
            set => _isOut = value;
        }
        public int Bank
        {
            get => _bank;
            set => _bank = value;
        }
        public int Bet
        {
            get => _bet;
            set => _bet = value;
        }
        public int Position
        {
            get => _position;
            set => _position = value;
        }
    }
}