﻿using System;
using System.Text;

namespace DurakSrcLibrary
{
    public class Card : IEquatable<Card>, IComparable<Card>
    {
        #region field variable for Card Class
        // represent the suit of the card
        private CardSuit fCardSuit;
        // represent the rank of the card
        private CardRank fCardRank;
        // represent the card status to be a special card
        private bool fIsTrumpCard;
        // whether the card is face up or not
        private bool fFaceUp = false;
        private string fImage;
        #endregion

        #region Encapsulate field properties

        /// <summary>
        /// Get Property for Card Suit
        /// </summary>
        public CardSuit CardSuitProperty { get => fCardSuit; set => fCardSuit = value; }
        /// <summary>
        /// Get Property for Card Rank
        /// </summary>
        public CardRank CardRankProperty { get => fCardRank; set => fCardRank = value; }

        /// <summary>
        /// Get and Set Property for Trump card
        /// </summary>
        public bool IsTrumpCardProperty { get => fIsTrumpCard; set => fIsTrumpCard = value; }
        public bool FaceUp
        {
            get => fFaceUp;
            set
            {
                fFaceUp = value;
                fImage = GetCardImage();
            }

        }
        public string CardImage { get => fImage; }
        #endregion

        // to initialise the default value to the default constructor
        //private const CardSuit DEFAULT_CARD_SUIT = CardSuit.Heart;
        //private const CardRank DEFAULT_CARD_RANK = CardRank.Ace;

        #region Constructor
        /// <summary>
        /// defalut constructor 
        /// </summary>
        public Card()
        {
            fIsTrumpCard = false;
            fImage = GetCardImage();
        }

        /// <summary>
        /// constructor with arguments to initialise the card value
        /// </summary>
        /// <param name="aCardSuit"><see cref = "CardSuit"/></param>
        /// <param name="aCardRank"><see cref = "CardRank"/></param>
        public Card(CardSuit aCardSuit, CardRank aCardRank)
        {
            // Initialise the suit of the current card
            fCardSuit = aCardSuit;
            // Initialise the rank of the current card
            fCardRank = aCardRank;
            // default status for trump card
            fIsTrumpCard = false;

            fImage = GetCardImage();


        }
        #endregion

        #region operator overloading

        public override bool Equals(object aObj)
        {
            return Equals(aObj as Card);
        }

        public bool Equals(Card aOther)
        {
            // Check if the card is not null and compare the rank and suit of the card
            return !(aOther is null) && (fCardSuit == aOther.fCardSuit) && (fCardRank == aOther.fCardRank);
        }

        public int CompareTo(Card other)
        {
            if (other is null)
            {
                return 1;
            }

            Card lCard = other as Card;
            if (!(lCard is null))
            {
                return this.fCardRank.CompareTo(other.fCardRank);
            }
            else
            {
                throw new ArgumentException("Object is not a Card");
            }
        }

        /// <summary>
        /// compare two card with their suit and rank
        /// </summary>
        /// <param name="card1"></param>
        /// <param name="card2"></param>
        /// <returns></returns>
        public static bool operator !=(Card aCard1, Card aCard2)
        {
            return !(aCard1 == aCard2);
        }

        /// <summary>
        /// compare two card with their suit and rank
        /// </summary>
        /// <param name="aCard1"></param>
        /// <param name="aCard2"></param>
        /// <returns></returns>
        public static bool operator ==(Card aCard1, Card aCard2)
        {
            // Check if the both card is not null and compare the rank and suit of the cards
            return !(aCard1 is null) && !(aCard2 is null) && (aCard1.fCardSuit == aCard2.fCardSuit) && (aCard1.fCardRank == aCard2.fCardRank);
        }

        /// <summary>
        /// check if card1 is greater than card2
        /// </summary>
        /// <param name="aCard1"></param>
        /// <param name="aCard2"></param>
        /// <returns></returns>
        public static bool operator >(Card aCard1, Card aCard2)
        {
            // If both Cards are not null
            if (!(aCard1 is null) && !(aCard2 is null))
            {
                // If both cards have same suit
                if (aCard1.fCardSuit == aCard2.fCardSuit)
                {
                    // If Card1 is Ace and Card2 is not Ace
                    if (aCard1.fCardRank == CardRank.Ace && aCard2.fCardRank != CardRank.Ace)
                    {
                        return true;
                    }
                    // If Card1 is not Ace and Card2 is Ace
                    else if (aCard1.fCardRank != CardRank.Ace && aCard2.fCardRank == CardRank.Ace)
                    {
                        return false;
                    }
                    else
                    {
                        // Compare the rank for the remainig possiblities
                        return aCard1.fCardRank > aCard2.fCardRank;
                    }
                }
                // If both cards have different suit
                else
                {
                    // If Card1 is trump card and Card2 is not trump card
                    if (aCard1.fIsTrumpCard == true && aCard2.fIsTrumpCard == false)
                    {
                        return true;
                    }
                    // If Card1 is not trump card and Card2 is trump card
                    else if (aCard1.fIsTrumpCard == false && aCard2.fIsTrumpCard == true)
                    {
                        return false;
                    }
                    // If Card1 is trump card and Card2 is also trump card
                    else if (aCard1.fIsTrumpCard == true && aCard2.fIsTrumpCard == true)
                    {
                        // If rank is same for both cards
                        if (aCard1.CardRankProperty == aCard2.CardRankProperty)
                        {
                            return true;
                        }
                        // Compare the rank of both the trump cards
                        return aCard1.fCardRank > aCard2.fCardRank;
                    }
                    else if (aCard1.fIsTrumpCard == false && aCard2.fIsTrumpCard == false)
                    {
                        // If rank is same for both cards
                        if (aCard1.CardRankProperty == aCard2.CardRankProperty)
                        {
                            return true;
                        }
                    }

                }
                // for any other possibility return false
                return false;
            }
            // Throw exception is any of the card is null
            throw new ArgumentNullException($"Cards Cannot be null while comparing: \nCard1: {aCard1.ToString()},\nCard2: {aCard2.ToString()}");
        }

        /// <summary>
        /// check if card2 is greater than card1
        /// </summary>
        /// <param name="aCard1"></param>
        /// <param name="aCard2"></param>
        /// <returns></returns>
        public static bool operator <(Card aCard1, Card aCard2)
        {
            return !(aCard1 > aCard2);
        }
        #endregion

        #region Method Helpers
        /// <summary>
        /// Get the Card Name related to the resource file name
        /// </summary>
        /// <returns>Resource File name</returns>
        private string GetImageName()
        {
            int lCardValue = (int)fCardRank;
            string lImageName;
            switch (fCardRank)
            {
                case CardRank.Ace:
                    lImageName = "A";
                    break;
                case CardRank.King:
                    lImageName = "K";
                    break;
                case CardRank.Queen:
                    lImageName = "Q";
                    break;
                case CardRank.Jack:
                    lImageName = "J";
                    break;
                default:
                    lImageName = "_" + lCardValue.ToString();
                    break;
            }
            switch (fCardSuit)
            {
                case CardSuit.Club:
                    lImageName += "C";
                    break;
                case CardSuit.Diamond:
                    lImageName += "D";
                    break;
                case CardSuit.Heart:
                    lImageName += "H";
                    break;
                case CardSuit.Spade:
                    lImageName += "S";
                    break;
                default:
                    lImageName += "?";
                    break;
            }

            return lImageName;
        }
        public void TurnOver()
        {
            fFaceUp = !fFaceUp;
            fImage = GetCardImage();
        }
        public string GetCardImage()
        {
            //the name of the image in the resource file 
            string lImageName;
            // holds the image
            //Image lCardImage;
            if (!fFaceUp)
            {
                // set the image name to back
                lImageName = "back";
            }
            else
            {
                lImageName = GetImageName();
            }
            //lCardImage = Properties.Resources.ResourceManager.GetObject(lImageName) as Image;
            return lImageName;
        }
        #endregion

        /// <summary>
        /// Get the hash code for the current instance
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() // reference from https://docs.microsoft.com/en-us/visualstudio/ide/reference/generate-equals-gethashcode-methods?view=vs-2019 to generate hashcode
        {
            var lhashCode = 352033288;
            lhashCode = lhashCode * (-1521134295) + fCardRank.GetHashCode();
            lhashCode = lhashCode * (-1521134295) + fCardSuit.GetHashCode();
            return lhashCode;
        }

        /// <summary>
        /// Converts the value of this instance to its equivalent string representation
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder lBuilder = new StringBuilder();
            return lBuilder.Append("Card{ Rank = ")
                    .Append(fCardRank.ToString())
                    .Append(",\tSuit = ")
                    .Append(fCardSuit.ToString())
                    .Append(",\tTrump Card = ")
                    .Append(fIsTrumpCard.ToString())
                    .Append("}").ToString();
        }
    }
}
