using CardsAPI.models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CardsAPI.Helpers
{
    //Deckhelper class
    //Class responsible for creation of deck
    public class DeckHelper
    {
        //creation of random deck
        public Deck CreateRandomDeck()
        {
          
            //creation of deck
            Deck d = new Deck();

            Enum[] suits = new Enum[] { suit.Clubs, suit.Heart, suit.Diamond, suit.Spades };

            //creation of cards
            foreach(suit s in suits)
            {
                AddCardsFace(d, s);
                AddCardsRegular(d, s);
            }


            List<Card> cardlist = d.cards.ToList();
            
            //intialize cards position
            for(int i = 0; i < cardlist.Count; i++)
            {
                cardlist[i].position = i;
            }

            //shuffle deck
            Deck sd = ShuffleDeck(d);
            return sd;
     
        }
        //Add face cards

        public Deck AddCardsFace(Deck d,suit s)
        {

            Enum[] faces = new Enum[]{ face.Jack, face.Queen, face.King, face.Ace};
            int cardVal = 11;

            foreach(face i in faces) {
                Card c = new Card();
                c.suit = s;
                c.face = i;
                c.value = cardVal;
                c.position = 0;
                cardVal++;
                d.cards.Add(c);    
            }

            return d;

        }
        //Add cards from 2-10
        public Deck AddCardsRegular(Deck d,suit suit) {
            for(int i = 2; i < 11; i++)
            {
                Card c = new Card();
                c.suit = suit;
                c.face = face.Regular;
                c.value = 0;
                d.cards.Add(c);
                c.position = i;
            }
            return d;
        }

        //shuffle deck
        //takes in a deck object
        public Deck ShuffleDeck(Deck d)
        {
            List<Card> cardlist = d.cards.Where(c => c.player_id == null).ToList();
            for (int i = 0; i < cardlist.Count; i++)
            {
                if (cardlist.Count != 1) {
                    Random rand = new Random();
                    int index = rand.Next(1, d.cards.Count);
                    int temp = cardlist[index].position;
                    cardlist[index].position = cardlist[i].position;
                    cardlist[i].position = temp;
                }
            }

            d.cards = cardlist;
            return d;

        }

        //helper responsible for shuffling list of cards
        //takes in a list of cards
        public List<Card> ShuffleCards(List<Card> cards)
        {
            List<Card> getUnclaimedCards = cards.Where(c => c.player_id == null).ToList();
            for (int i = 0; i < getUnclaimedCards.Count; i++)
            {
                if (getUnclaimedCards.Count != 1)
                {
                    Random rand = new Random();
                    int index = rand.Next(1, getUnclaimedCards.Count);
                    int temp = getUnclaimedCards[index].position;
                    getUnclaimedCards[index].position = getUnclaimedCards[i].position;
                    getUnclaimedCards[i].position = temp;
                }
            }

            cards = getUnclaimedCards;
            return cards;

        }



    }
}
