using PIACore.Kernel;
using PIACore.Model;
using PIACore.Model.Enums;

namespace PIACore.AI
{
    public class AlwaysCallAIManager : IAiManager
    {
        
        public Play playAction(Table table)
        {
            return new Play(PlayType.call, 0);
        }
    }
}