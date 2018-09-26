using System;
using System.Collections.Generic;
using System.Threading;
using PIACore.Containers;
using PIACore.Helpers;
using PIACore.Model;
using PIACore.Model.Enums;
using PIACore.Web;

namespace PIACore.Kernel
{
    /// <summary>
    /// A game is a physical instance of a playing session on multiple tables.
    /// </summary>
    /// <typeparam name="T">The instance of the IAiManager that will play the game</typeparam>
    public class Game<T> where T : class, IAiManager, new()
    {
        /// <summary>
        /// The list of all table containers used in this game session.
        /// </summary>
        private Dictionary<string, TableContainer> tables = new Dictionary<string, TableContainer>();

        /// <summary>
        /// The APIConnector used to interact with Poker Online.
        /// </summary>
        private ApiConnector connector;

        /// <summary>
        /// Instanciate a game with a specific API Connector.
        /// </summary>
        public Game()
        {
            connector = new ApiConnector();
        }

        /// <summary>
        /// Run the game indefinitely
        /// </summary>
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
                    foreach (var table in tables)
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
                                Logger.Info("AI played : " + play.PlayType + (play.PlayType == PlayType.Raise ? " and raised "+play.Amount : ""));
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

        /// <summary>
        /// Update all tables of the current game, removing those that are over, and joining those that have the good tag.
        /// </summary>
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

                tables.Add(tableId, container);
            }

            //Remove unused tables from the list :
            var currentTables = connector.currentTables();

            foreach (var tableId in tables.Keys)
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
                if (!tables.ContainsKey(currentOnlineTables))
                {
                    TableContainer container = new TableContainer
                    {
                        AiManager = new T(), Table = new Table(), TableId = currentOnlineTables
                    };

                    tables.Add(currentOnlineTables, container);
                }
            }
        }
    }
}