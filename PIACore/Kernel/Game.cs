using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PIACore.Containers;
using PIACore.Log;
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
            string playerId = connector.getId();
            while (true)
            {
                bool failure = false;
                try
                {
                    // Join all tables :
                    updateTables();
                }
                catch (Exception e)
                {
                    Logger.Error("Error : failure on tables update...");
                    Logger.Error(e.Message + " --- " + e.StackTrace);
                    failure = true;
                }

                if (!failure)
                {
                    foreach (var table in _tables)
                    {
                        Play play = null;
                        Table tableModel = null;
                        Logger.Info("Initiating turn on table [" + table.Key + "]");

                        try
                        {
                            tableModel = connector.getTableState(table.Key, playerId);
                        }
                        catch (Exception e)
                        {
                            Logger.Error("Error : getting table model failed...");
                            Logger.Error(e.Message + " --- " + e.StackTrace);
                        }

                        if (tableModel != null)
                        {
                            play = table.Value.AiManager.playAction(tableModel);
                        }
                        else
                        {
                            Logger.Info("Turn is not mine...");
                        }

                        try
                        {
                            if (play != null)
                            {
                                Logger.Info("AI played : " + play.PlayType);
                                connector.playTurn(play.PlayType, table.Key, play.Amount);
                            }
                            else
                            {
                                Logger.Info("AI gave no turn indications...");
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Error("Error : instructing game action failed...");
                            Logger.Error(e.Message + " --- " + e.StackTrace);
                        }
                    }
                }
                Thread.Sleep(int.Parse(Environment.GetEnvironmentVariable("AI_REFRESH_RATE_IN_MILIS")));
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

            // Join tables that were not joined before :
            foreach (var currentOnlineTables in currentTables)
            {
                if (!_tables.ContainsKey(currentOnlineTables))
                {
                    TableContainer container = new TableContainer
                    {
                        AiManager = new T(), Table = new Table(), TableId = currentOnlineTables
                    };

                    _tables.Add(currentOnlineTables, container);
                }
            }
        }
    }
}