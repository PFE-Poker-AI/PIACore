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
            joinTables();
            
            // Pour chaque table
            foreach (var table in _tables)
            {
                //Recuperer l'etat :
                
                //Soit jouer
                
                //Soit fermer la table (fin de partie)
            }
        }


        private void joinTables()
        {
            //IF we find a table to join
            IAiManager manager = new T();
            
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