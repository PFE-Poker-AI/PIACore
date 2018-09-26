using PIACore.Kernel;
using PIACore.Model;
using PIACore.Model.Enums;

namespace PIACore.AI
{
    /// <summary>
    /// This class is an example implementation of a call AI. It will simply always call.
    /// </summary>
    public class AlwaysCallAIManager : IAiManager
    {
        
        public Play playAction(Table table)
        {
            return new Play(PlayType.Call, 0);
        }
    }
}