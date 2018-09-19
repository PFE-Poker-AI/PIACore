using PIACore.Model;

namespace PIACore.Kernel
{
    public interface IAiManager
    {
        Play playAction(Table table);

        void gameFinishedNotify();
    }
}