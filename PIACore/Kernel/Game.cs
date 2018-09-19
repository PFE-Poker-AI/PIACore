using System.Collections.Generic;
using PIACore.Model;

namespace PIACore.Kernel
{
    public class Game<T> where T : class, IAiManager, new()
    {
        private Dictionary<string, TableAi> _tables = new Dictionary<string, TableAi>();

        public void Run()
        {
            // Regarder les tables
            updateTables();

            // Pour chaque table
            foreach (var table in _tables)
            {
                //Recuperer l'etat :

                //Soit jouer

                //Soit fermer la table (fin de partie)
            }
        }

        private void updateTables()
        {
            // Get all tables

            //IF we find a table to join :
            TableAi manager = new TableAi
            {
                AiManager = new T(), Table = new Table(),
            };

            _tables.Add("table key", manager);

            //IF we find a table that is closed :
        }
    }

    public class TableAi
    {
        private IAiManager _aiManager;

        private Table _table;

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
    }
}