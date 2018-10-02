using PIACore.Kernel;
using PIACore.Model;
using PIACore.Model.Enums;

namespace PIACore.AI.AlwaysCallAI
{
    public class AlwaysCallAIManager : IAiManager
    {
        public Play PlayAction(Table table, string slug)
        {
            return new Play(PlayType.Call, 0);
        }
    }
}