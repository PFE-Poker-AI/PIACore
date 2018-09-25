using PIACore.Model.Enums;

namespace PIACore.Model
{
    public class Play
    {
        private int _amount;
        private PlayType _playType;
    
        public Play(PlayType play, int amount) {
            _amount = amount;
            _playType = play;
        }

        public int Amount
        {
            get => _amount;
            set => _amount = value;
        }
        public PlayType PlayType
        {
            get => _playType;
            set => _playType = value;
        }
    }
}