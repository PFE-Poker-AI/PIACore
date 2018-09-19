namespace PIACore.Model
{
    public class Card
    {
        private int value;
        private int color;

        public int Value
        {
            get => value;
            set => this.value = value;
        }
        public int Color
        {
            get => color;
            set => color = value;
        }
    }
}