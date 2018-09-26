using PIACore.Model.Enums;

namespace PIACore.Model
{
    /// <summary>
    /// This model represents a Play of poker.
    /// </summary>
    public class Play
    {
        private int amount;
        private PlayType playType;
    
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="play">The play type.</param>
        /// <param name="amount">The amount value.</param>
        public Play(PlayType play, int amount) {
            Amount = amount;
            PlayType = play;
        }

        /// <summary>
        /// The amount to play (only used when raising).
        /// </summary>
        public int Amount
        {
            get => amount;
            set => amount = value;
        }
        
        /// <summary>
        /// The play type (call, fold, raise).
        /// </summary>
        public PlayType PlayType
        {
            get => playType;
            set => playType = value;
        }
    }
}