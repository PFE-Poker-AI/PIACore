using System.Collections.Generic;
using PIACore.Exceptions;
using PIACore.Model.Enums;

namespace PIACore.Model
{
    /// <summary>
    /// This model represents a poker Card.
    /// </summary>
    public class Card
    {
        private CardValue _value;
        private CardColor _color;

        /// <summary>
        /// The card value.
        /// </summary>
        public CardValue Value
        {
            get => _value;
            set => this._value = value;
        }
        
        /// <summary>
        /// The card color.
        /// </summary>
        public CardColor Color
        {
            get => _color;
            set => _color = value;
        }

        /// <summary>
        /// Create a List of cards from a given List of object (issued from a json response).
        /// </summary>
        /// <param name="jsonCards">The json response.</param>
        /// <returns>A list of Cards.</returns>
        /// <exception cref="WrongValuePokerException">Thrown when a received value could not be mapped.</exception>
        /// <exception cref="WrongColorPokerException">Thrown when a received color could not be mapped.</exception>
        public static List<Card> CreateFromJsonList(List<object> jsonCards)
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