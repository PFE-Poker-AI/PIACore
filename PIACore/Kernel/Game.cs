using System.Collections.Generic;
using System.Linq;
using PIACore.Containers;
using PIACore.Model;
using PIACore.Web;

namespace PIACore.Kernel
{
    public class Game<T> where T : class, IAiManager, new()
    {
        private Dictionary<string, TableContainer> _tables = new Dictionary<string, TableContainer>();

        private ApiConnector connector;

        public Game()
        {
            connector = new ApiConnector();
        }

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
            //Join new tables :
            var tableIds = connector.getNewTableIds();

            //Foreach table to join :
            foreach (var tableId in tableIds)
            {
                connector.joinGivenTable(tableId);

                TableContainer container = new TableContainer
                {
                    AiManager = new T(), Table = new Table(), TableId = tableId
                };
                
                _tables.Add(tableId, container);
            }
            
            //Remove unused tables from the list :
            var currentTables = connector.currentTables();
            
            foreach (var tableId in _tables.Keys)
            {
                if (!currentTables.Contains(tableId))
                {
                    // Remove the table :
                    currentTables.Remove(tableId);
                }
            }
        }
    }
}