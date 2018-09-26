using PIACore.Model;

namespace PIACore.Kernel
{
    /// <summary>
    /// Implement this interface to create your own AI.
    /// </summary>
    public interface IAiManager
    {
        /// <summary>
        /// This method is called everytime the gamestate allows the AI to play a hand.
        /// </summary>
        /// <param name="table">The Table that represent the game at a given time.</param>
        /// <returns>The action that will be played by the AI.</returns>
        Play playAction(Table table);
    }
}