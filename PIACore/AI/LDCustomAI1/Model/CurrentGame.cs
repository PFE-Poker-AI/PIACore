namespace PIACore.AI.LDCustomAI1.Model
{
    public class CurrentGame
    {
        private int _handStrenght = 0;

        public int HandStrenght
        {
            get => _handStrenght;
            set => _handStrenght = value;
        }

        private bool _isInitiated = false;

        public bool IsInitiated
        {
            get => _isInitiated;
            set => _isInitiated = value;
        }

        private bool _isFolded = false;

        public bool IsFolded
        {
            get => _isFolded;
            set => _isFolded = value;
        }


        private bool _isBluff = false;

        public bool IsBluff
        {
            get => _isBluff;
            set => _isBluff = value;
        }


        private int _gameStep;

        public int GameStep
        {
            get => _gameStep;
            set => _gameStep = value;
        }
    }
}