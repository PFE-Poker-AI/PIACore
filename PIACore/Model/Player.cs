namespace PIACore.Model
{
    public class Player
    {
        private bool _isSelf;

        private PlayerTurn _currentTurn;

        private Card[] _cards = new Card[2];

        private Table _table;

        public bool IsSelf
        {
            get => _isSelf;
            set => _isSelf = value;
        }
        public PlayerTurn CurrentTurn
        {
            get => _currentTurn;
            set => _currentTurn = value;
        }
        public Card[] Cards
        {
            get => _cards;
            set => _cards = value;
        }
        public Table Table
        {
            get => _table;
            set => _table = value;
        }
    }
}