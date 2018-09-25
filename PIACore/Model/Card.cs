using System.Collections.Generic;
using PIACore.Exceptions;
using PIACore.Model.Enums;

namespace PIACore.Model
{
    public class Card
    {
        private CardValue value;
        private CardColor color;

        public CardValue Value
        {
            get => value;
            set => this.value = value;
        }
        public CardColor Color
        {
            get => color;
            set => color = value;
        }

        public static List<Card> createFromJsonList(List<object> jsonCards)
        {
            var cards = new List<Card>();

            foreach (var element in jsonCards)
            {
                var cardElement = (Dictionary<string, object>)element;

                var jsonCardValue = (string) cardElement["rank"];
                var jsonCardColor = (string) cardElement["suit"];
                CardValue value;
                CardColor color;

                switch (jsonCardValue)
                {
                    case "2":
                        value = CardValue.Two;
                        break;
                    case "3":
                        value = CardValue.Three;
                        break;
                    case "4":
                        value = CardValue.Four;
                        break;
                    case "5":
                        value = CardValue.Five;
                        break;
                    case "6":
                        value = CardValue.Six;
                        break;
                    case "7":
                        value = CardValue.Seven;
                        break;
                    case "8":
                        value = CardValue.Eight;
                        break;
                    case "9":
                        value = CardValue.Nine;
                        break;
                    case "10":
                        value = CardValue.Ten;
                        break;
                    case "J":
                        value = CardValue.Jack;
                        break;
                    case "Q":
                        value = CardValue.Queen;
                        break;
                    case "K":
                        value = CardValue.King;
                        break;
                    case "A":
                        value = CardValue.Ace;
                        break;
                    default:
                        throw new WrongValuePokerException();
                }

                switch (jsonCardColor)
                {
                    case "diams":
                        color = CardColor.Diamond;
                        break;
                    case "hearts":
                        color = CardColor.Heart;
                        break;
                    case "spades":
                        color = CardColor.Spade;
                        break;
                    case "clubs":
                        color = CardColor.Club;
                        break;
                    default:
                        throw new WrongColorPokerException();
                }

                cards.Add(
                    new Card
                    {
                        Color = color, Value = value
                    }
                );
            }

            return cards;
        }
    }
}