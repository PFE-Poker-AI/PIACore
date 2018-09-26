namespace PIACore.Model
{
    public class PlayerTurn
    {
        private bool isOut;
        private int bank;
        private int bet;
        private int position;
        
        public bool IsOut
        {
            get => isOut;
            set => isOut = value;
        }
        public int Bank
        {
            get => bank;
            set => bank = value;
        }
        public int Bet
        {
            get => bet;
            set => bet = value;
        }
        public int Position
        {
            get => position;
            set => position = value;
        }
    }
}