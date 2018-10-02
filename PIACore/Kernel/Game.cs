using System;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
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
        /// Name of the current Game AI.
        /// </summary>
        private string _slug;

        /// <summary>
        /// Time in milliseconds that the AI will wait.
        /// </summary>
        private int _timeInMillis;

        /// <summary>
        /// Table string identifier.
        /// </summary>
        private string _tableIdentifier;

        /// <summary>
        /// The list of all table containers used in this game session.
        /// </summary>
        private Dictionary<string, TableContainer> _tables = new Dictionary<string, TableContainer>();

        /// <summary>
        /// The APIConnector used to interact with Poker Online.
        /// </summary>
        private ApiConnector _connector;

        /// <summary>
        /// Instanciate a game with a specific API Connector.
        /// </summary>
        public Game(string tableIdentifier, int timeInMillis, string slug, string apiKey)
        {
            this._tableIdentifier = tableIdentifier;
            this._timeInMillis = timeInMillis;
            this._slug = slug;
            _connector = new ApiConnector(apiKey, tableIdentifier);
        }
        
        /// <summary>
        /// Run the game indefinitely
        /// </summary>
        [UsedImplicitly]
        public void Run()
        {
            string playerId = _connector.GetId();
            bool failure = false;
            while (true)
            {
                try
                {
                    // Join all tables :
                    UpdateTables();
                }
                catch (Exception e)
                {
                    Logger.Error("Error : failure on tables update...", _slug);
                    Logger.Error(e.Message + " --- " + e.StackTrace, _slug);
                    failure = true;
                }

                if (!failure)
                {
                    foreach (var table in _tables)
                    {
                        Play play = null;
                        Table tableModel = null;
                        Logger.Info("Initiating turn on table [" + table.Key + "]", _slug);

                        try
                        {
                            tableModel = _connector.GetTableState(table.Key, playerId);
                        }
                        catch (Exception e)
                        {
                            Logger.Error("Error : getting table model failed...", _slug);
                            Logger.Error(e.Message + " --- " + e.StackTrace, _slug);
                        }

                        if (tableModel != null)
                        {
                            play = table.Value.AiManager.PlayAction(tableModel, _slug);
                        }
                        else
                        {
                            Logger.Info("Turn is not mine...", _slug);
                        }

                        try
                        {
                            if (play != null)
                            {
                                Logger.Info("AI played : " + play.PlayType +
                                            (play.PlayType == PlayType.Raise ? " and raised " + play.Amount : ""),
                                    _slug);
                                _connector.PlayTurn(play.PlayType, table.Key, play.Amount);
                            }
                            else
                            {
                                Logger.Info("AI gave no turn indications...", _slug);
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Error("Error : instructing game action failed...", _slug);
                            Logger.Error(e.Message + " --- " + e.StackTrace, _slug);
                        }
                    }
                }

                Thread.Sleep(_timeInMillis);
            }
        }

        /// <summary>
        /// Update all tables of the current game, removing those that are over, and joining those that have the good tag.
        /// </summary>
        private void UpdateTables()
        {
            //Join new tables :
            var tableIds = _connector.GetNewTableIds();

            //Foreach table to join :
            foreach (var tableId in tableIds)
            {
                _connector.JoinGivenTable(tableId);

                TableContainer container = new TableContainer
                {
                    AiManager = new T(), Table = new Table(), TableId = tableId
                };

                _tables.Add(tableId, container);
            }

            //Remove unused tables from the list :
            var currentTables = _connector.CurrentTables();

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