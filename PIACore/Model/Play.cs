using PIACore.Model.Enums;

namespace PIACore.Model
{
    public class Play
    {
        private int amount;
        private PlayType play;
    
        public Play(PlayType play, int amount) {
            this.amount = amount;
            this.play = play;
        }

    }
}