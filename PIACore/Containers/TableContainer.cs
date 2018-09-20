using PIACore.Kernel;
using PIACore.Model;

namespace PIACore.Containers
{
    public class TableContainer
    {
        private IAiManager _aiManager;

        private Table _table;

        private string _tableId;

        public IAiManager AiManager
        {
            get => _aiManager;
            set => _aiManager = value;
        }
        
        public Table Table
        {
            get => _table;
            set => _table = value;
        }

        public string TableId
        {
            get => _tableId;
            set => _tableId = value;
        }
    }
}