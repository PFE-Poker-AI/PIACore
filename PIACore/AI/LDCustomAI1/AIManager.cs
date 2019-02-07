using System;
using System.Collections.Generic;
using System.Linq;
using PIACore.AI.LDCustomAI1.Helpers;
using PIACore.AI.LDCustomAI1.Model;
using PIACore.Exceptions;
using PIACore.Helpers;
using PIACore.Kernel;
using PIACore.Model;
using PIACore.Model.Enums;

namespace PIACore.AI.LDCustomAI1
{
    public class AIManager : IAiManager
    {
        private int gamesOccured = 0;
        private CurrentGame _currentGame = new CurrentGame();
        private int currentPot; //In small Blinds
        private int handPower;
        private Player selfPlayer;

        public Play PlayAction(Table table, string slug)
        {
            selfPlayer = table.Players.FirstOrDefault(player => player.Value.IsSelf).Value;

            handPower = PokerEvaluation.HandPower(selfPlayer.Cards, table.Cards);

            currentPot = table.Pot / table.SmallBlindValue;
            // New game :
            if (table.GamesOccured > gamesOccured)
            {
                _currentGame = new CurrentGame();
                gamesOccured = table.GamesOccured;
            }

            if (!_currentGame.IsInitiated)
            {
                Initiate(selfPlayer.Cards);
                _currentGame.IsInitiated = true;
            }

            _currentGame.GameStep = EvaluateGameStep(table);

            if (_currentGame.IsFolded)
            {
                return new Play(PlayType.Fold);
            }

            if (_currentGame.GameStep == 0)
            {
                Logger.Info("Playing Preflop");
                return PlayPreFlop(table);
            }

            if (_currentGame.GameStep == 1)
            {
                Logger.Info("Playing Flop");
                return PlayFlop(table);
            }

            if (_currentGame.GameStep == 2)
            {
                Logger.Info("Playing Turn");
                return PlayTurn(table);
            }

            if (_currentGame.GameStep == 3)
            {
                Logger.Info("Playing River");
                return PlayRiver(table);
            }

            return new Play(PlayType.Fold);
        }

        public int EvaluateGameStep(Table table)
        {
            switch (table.Cards.Count)
            {
                case 0:
                    return 0;
                case 3:
                    return 1;
                case 4:
                    return 2;
                case 5:
                    return 3;
                default:
                    throw new WrongCardAmountException();
            }
        }

        public void Initiate(List<Card> cards)
        {
            //Handle bluff :
            if (_currentGame.GameStep == 0)
            {
                Random rnd = new Random();
                if (rnd.Next(1, 100) <= 7)
                {
                    _currentGame.IsBluff = true;
                }
            }

            _currentGame.HandStrenght = PokerEvaluation.GetNashValueOfHand(cards);
        }

        public Play PlayPreFlop(Table table)
        {
            var rnd = new Random();

            if (_currentGame.IsBluff)
            {
                if (rnd.Next(0, 3) == 3)
                {
                    return new Play(PlayType.Raise, 1500 * table.SmallBlindValue);
                }

                return new Play(PlayType.Raise, rnd.Next(1, 5) * table.SmallBlindValue);
            }

            if (_currentGame.HandStrenght > 500)
            {
                if (currentPot < 10 * table.SmallBlindValue)
                {
                    return new Play(PlayType.Raise, rnd.Next(8, 16) * table.SmallBlindValue);
                }
            }

            if (_currentGame.HandStrenght > 100 && currentPot < 30)
            {
                return new Play(PlayType.Call);
            }

            if (_currentGame.HandStrenght > 200 && currentPot < 50)
            {
                return new Play(PlayType.Call);
            }

            if (_currentGame.HandStrenght > 400 && currentPot < 125)
            {
                return new Play(PlayType.Call);
            }

            foreach (var player in table.Players)
            {
                if (player.Value.Bid > 4 && !player.Value.IsSelf)
                {
                    return new Play(PlayType.Fold);
                }
            }

            return new Play(PlayType.Call);
        }

        public Play PlayFlop(Table table)
        {
            var rnd = new Random();
            if (_currentGame.IsBluff)
            {
                if (rnd.Next(0, 3) == 3)
                {
                    return new Play(PlayType.Raise, 1500 * table.SmallBlindValue);
                }

                return new Play(PlayType.Raise, rnd.Next(1, 5) * table.SmallBlindValue);
            }

            if (handPower > 2)
            {
                var choice = rnd.Next(0, 1);
                if (choice == 1)
                {
                    return new Play(PlayType.Call);
                }

                return new Play(PlayType.Raise, 1500 * table.SmallBlindValue);
            }

            if (handPower > 1)
            {
                var choice = rnd.Next(0, 5);
                if (choice == 1)
                {
                    return new Play(PlayType.Call);
                }

                return new Play(PlayType.Raise, 1500 * table.SmallBlindValue);
            }

            if (handPower > 0)
            {
                if (currentPot < 150)
                {
                    return new Play(PlayType.Call);
                }
            }

            if (currentPot < 30)
            {
                return new Play(PlayType.Call);
            }

            foreach (var player in table.Players)
            {
                if (player.Value.Bid > 4 && !player.Value.IsSelf)
                {
                    return new Play(PlayType.Fold);
                }
            }

            return new Play(PlayType.Call);
        }

        public Play PlayTurn(Table table)
        {
            var rnd = new Random();
            if (_currentGame.IsBluff)
            {
                if (rnd.Next(0, 3) == 3)
                {
                    return new Play(PlayType.Raise, 1500 * table.SmallBlindValue);
                }

                return new Play(PlayType.Raise, rnd.Next(1, 5) * table.SmallBlindValue);
            }

            if (handPower > 2)
            {
                return new Play(PlayType.Call);
            }

            if (handPower > 0)
            {
                if (handPower > 1 && currentPot < 60)
                {
                    return new Play(PlayType.Call);
                }

                if (currentPot < 40)
                {
                    return new Play(PlayType.Call);
                }
            }

            foreach (var player in table.Players)
            {
                if (player.Value.Bid > 4 && !player.Value.IsSelf)
                {
                    return new Play(PlayType.Fold);
                }
            }

            return new Play(PlayType.Call);
        }

        public Play PlayRiver(Table table)
        {
            var rnd = new Random();
            if (_currentGame.IsBluff)
            {
                return new Play(PlayType.Raise, 1500 * table.SmallBlindValue);
            }

            if (handPower > 2)
            {
                return new Play(PlayType.Raise, 25);
            }

            if (handPower > 1)
            {
                if (currentPot < 80)
                {
                    return new Play(PlayType.Call);
                }
            }

            if (handPower > 0)
            {
                bool betFollow = true;
                foreach (var player in table.Players)
                {
                    if (player.Value.Bid > 20 && !player.Value.IsSelf)
                    {
                        betFollow = false;
                    }
                }

                if (betFollow)
                    return new Play(PlayType.Call);
            }

            foreach (var player in table.Players)
            {
                if (player.Value.Bid > 20 && !player.Value.IsSelf)
                {
                    return new Play(PlayType.Fold);
                }
            }

            return new Play(PlayType.Call);
        }
    }
}