using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PIACore.Exceptions;
using PIACore.Model;
using PIACore.Model.Enums;

namespace PIACore.AI.LDCustomAI1.Helpers
{
    public static class PokerEvaluation
    {
        public static int GetNashValueOfHand(List<Card> cards)
        {
            if (cards?.Count != 2)
                throw new WrongCardAmountException();

            var firstCard = cards[0].Value >= cards[1].Value ? cards[0] : cards[1];
            var secondCard = cards[0].Value < cards[1].Value ? cards[0] : cards[1];

            bool suited = cards[0].Color == cards[1].Color;
            var suitedText = suited ? "Suited" : "";
            return handNashValue[firstCard.Value.ToString() + secondCard.Value.ToString() + suitedText];
        }

        public static int HandPower(List<Card> myCards, List<Card> boardCards)
        {
            int handValue = 0; // (100 = pair, 900 royal flush)


            bool flush = false;
            bool straight = false;
            int quads, triples, pairs = 0;


            var allCards = new List<Card>();
            allCards.AddRange(myCards);
            allCards.AddRange(boardCards);

            flush = HasFlush(allCards);
            straight = HasStraight(allCards);
            quads = QuadCount(allCards);
            triples = TripleCount(allCards);
            pairs = PairCount(allCards);

            //Straight flush
            if (flush && straight)
            {
                return 8;
            }
            // Quads
            if (quads > 0)
            {
                return 7;
            }
            // Full house
            if ((pairs > 0 && triples > 0) || triples > 1)
            {
                return 6;
            }
            // Flush
            if (flush)
            {
                return 5;
            }
            // Straight
            if (straight)
            {
                return 4;
            }
            // Triples
            if (triples > 0)
            {
                return 3;
            }
            // Double Pair
            if (pairs > 1)
            {
                return 2;
            }
            // Pair
            if (pairs > 0)
            {
                return 1;
            }
            // Air
            return 0;
        }

        /// <summary>
        /// Sklansky-Chubukov hand ranking dictionary
        /// </summary>
        private static Dictionary<string, int> handNashValue = new Dictionary<string, int>
        {
            {"AceTwo", 230}, {"AceThree", 240}, {"AceFour", 260}, {"AceFive", 280}, {"AceSix", 280}, {"AceSeven", 310},
            {"AceEight", 360}, {"AceNine", 410}, {"AceTen", 530}, {"AceJack", 680}, {"AceQueen", 960},
            {"AceKing", 1660}, {"AceAce", 10000},

            {"KingTwo", 100}, {"KingThree", 110}, {"KingFour", 110}, {"KingFive", 120}, {"KingSix", 130},
            {"KingSeven", 140},
            {"KingEight", 150}, {"KingNine", 180}, {"KingTen", 230}, {"KingJack", 250}, {"KingQueen", 290},
            {"KingKing", 4770}, {"AceKingSuited", 2770},

            {"QueenTwo", 57}, {"QueenThree", 63}, {"QueenFour", 68}, {"QueenFive", 75}, {"QueenSix", 81},
            {"QueenSeven", 85},
            {"QueenEight", 99}, {"QueenNine", 120}, {"QueenTen", 150}, {"QueenJack", 160}, {"QueenQueen", 2390},
            {"QueenKingSuited", 430}, {"QueenAceSuited", 1370},

            {"JackTwo", 34}, {"JackThree", 40}, {"JackFour", 45}, {"JackFive", 50}, {"JackSix", 54}, {"JackSeven", 63},
            {"JackEight", 74}, {"JackNine", 89}, {"JackTen", 120}, {"JackJack", 1600}, {"QueenJackSuited", 250},
            {"KingJackSuited", 360}, {"AceJackSuited", 920},

            {"TenTwo", 24}, {"TenThree", 27}, {"TenFour", 31}, {"TenFive", 35}, {"TenSix", 43}, {"TenSeven", 51},
            {"TenEight", 61}, {"TenNine", 74}, {"TenTen", 120}, {"JackTenSuited", 180}, {"QueenTenSuited", 220},
            {"KingTenSuited", 310}, {"AceTenSuited", 700},

            {"NineTwo", 18}, {"NineThree", 20}, {"NineFour", 22}, {"NineFive", 28}, {"NineSix", 35}, {"NineSeven", 43},
            {"NineEight", 51}, {"NineNine", 960}, {"TenNineSuited", 76}, {"JackNineSuited", 130},
            {"QueenNineSuited", 160}, {"KingNineSuited", 240}, {"AceNineSuited", 520},

            {"EightTwo", 14}, {"EightThree", 15}, {"EightFour", 19}, {"EightFive", 24}, {"EightSix", 30},
            {"EightSeven", 38},
            {"EightEight", 800}, {"NineEightSuited", 76}, {"TenEightSuited", 87}, {"JackEightSuited", 100},
            {"QueenEightSuited", 130}, {"KingEightSuited", 200}, {"AceEightSuited", 450},

            {"SevenTwo", 11}, {"SevenThree", 14}, {"SevenFour", 17}, {"SevenFive", 21}, {"SevenSix", 27},
            {"SevenSeven", 670},
            {"EightSevenSuited", 56}, {"NineSevenSuited", 61}, {"TenSevenSuited", 71}, {"JackSevenSuited", 86},
            {"QueenSevenSuited", 110}, {"KingSevenSuited", 190}, {"AceSevenSuited", 400},

            {"SixTwo", 11}, {"SixThree", 13}, {"SixFour", 16}, {"SixFive", 20}, {"SixSix", 580}, {"SevenSixSuited", 27},
            {"EightSixSuited", 45}, {"NineSixSuited", 50}, {"TenSixSuited", 60}, {"JackSixSuited", 74},
            {"QueenSixSuited", 110}, {"KingSixSuited", 170}, {"AceSixSuited", 350},

            {"FiveTwo", 11}, {"FiveThree", 13}, {"FiveFour", 16}, {"FiveFive", 490}, {"SixFiveSuited", 31},
            {"SevenFiveSuited", 33},
            {"EightFiveSuited", 36}, {"NineFiveSuited", 41}, {"TenFiveSuited", 50}, {"JackFiveSuited", 70},
            {"QueenFiveSuited", 100}, {"KingFiveSuited", 160}, {"AceFiveSuited", 360},

            {"FourTwo", 10}, {"FourThree", 12}, {"FourFour", 41}, {"FiveFourSuited", 24}, {"SixFourSuited", 24},
            {"SevenFourSuited", 26},
            {"EightFourSuited", 28}, {"NineFourSuited", 33}, {"TenFourSuited", 46}, {"JackFourSuited", 65},
            {"QueenFourSuited", 95}, {"KingFourSuited", 150}, {"AceFourSuited", 330},

            {"ThreeTwo", 9}, {"ThreeThree", 33}, {"FourThreeSuited", 17}, {"FiveThreeSuited", 19},
            {"SixThreeSuited", 19}, {"SevenThreeSuited", 20},
            {"EightThreeSuited", 22}, {"NineThreeSuited", 30}, {"TenThreeSuited", 42}, {"JackThreeSuited", 60},
            {"QueenThreeSuited", 89}, {"KingThreeSuited", 140}, {"AceThreeSuited", 310},

            {"TwoTwo", 240}, {"ThreeTwoSuited", 13}, {"FourTwoSuited", 14}, {"FiveTwoSuited", 16}, {"SixTwoSuited", 15},
            {"SevenTwoSuited", 16},
            {"EightTwoSuited", 21}, {"NineTwoSuited", 27}, {"TenTwoSuited", 38}, {"JackTwoSuited", 56},
            {"QueenTwoSuited", 83}, {"KingTwoSuited", 130}, {"AceTwoSuited", 290},
        };

        private static bool HasFlush(List<Card> cards)
        {
            if (cards.Count(element => element.Color == CardColor.Club) > 4 ||
                cards.Count(element => element.Color == CardColor.Spade) > 4 ||
                cards.Count(element => element.Color == CardColor.Heart) > 4 ||
                cards.Count(element => element.Color == CardColor.Diamond) > 4)
            {
                return true;
            }

            return false;
        }

        private static bool HasStraight(List<Card> cards)
        {
            foreach (var card in cards)
            {
                if (cards.Any(value => value.Value == card.Value + 1) &&
                    cards.Any(value => value.Value == card.Value + 2) &&
                    cards.Any(value => value.Value == card.Value + 3) &&
                    cards.Any(value => value.Value == card.Value + 4)
                )
                {
                    return true;
                }
            }

            return false;
        }

        private static int QuadCount(List<Card> cards)
        {
            int count = 0;
            var removedValues = new List<Card>();
            foreach (var card in cards)
            {
                if (cards.Count(value => value.Value == card.Value) == 4 && !removedValues.Any(value => value.Value == card.Value))
                {
                    count++;
                    removedValues.Add(new Card
                    {
                        Value = card.Value,
                        Color = CardColor.Heart
                    });
                }
            }

            return count;
        }

        private static int TripleCount(List<Card> cards)
        {
            int count = 0;
            var removedValues = new List<Card>();
            foreach (var card in cards)
            {
                if (cards.Count(value => value.Value == card.Value) == 3 && !removedValues.Any(value => value.Value == card.Value))
                {
                    count++;
                    removedValues.Add(new Card
                    {
                        Value = card.Value,
                        Color = CardColor.Heart
                    });
                }
            }

            return count;
        }

        private static int PairCount(List<Card> cards)
        {
            int count = 0;
            var removedValues = new List<Card>();
            foreach (var card in cards)
            {
                if (cards.Count(value => value.Value == card.Value) == 2 && !removedValues.Any(value => value.Value == card.Value))
                {
                    count++;
                    removedValues.Add(new Card
                    {
                        Value = card.Value,
                        Color = CardColor.Heart
                    });
                }
            }

            return count;
        }
    }
}