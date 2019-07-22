using CardsAPI.Context;
using CardsAPI.Helpers;
using CardsAPI.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardsAPI.ResultLineObjects;
using System.Diagnostics;

namespace CardsAPI.Services
{
    //Game Service
    //Responsible for main Game
    public class GameService
    {

        CardsContext _context;
        //Intialize context
        public GameService(CardsContext context)
        {
            _context = context;

        }
        //Create new Game
        public Game CreateNewGame(Game g)
        {
            _context.Games.Add(g);
            _context.SaveChanges();
            return g;

        }
        //Deletion of game
        public bool DeleteGame(int game_id)
        {
            Game game = _context.Games.Where(g => g.game_id == game_id).FirstOrDefault();
            List<Player> players = _context.Players.Where(p => p.game_id == game_id).ToList();
            List<Deck> decks = _context.Decks.Where(d => d.game_id == game_id).ToList();
            List<Deck> cards = _context.Decks.Where(c => c.deck_id == game_id).ToList();

            //delete players
            foreach (Player p in players){
                game.players.Remove(p);
            }
            //remove decks
            foreach (Deck d in decks)
            {
                game.decks.Remove(d);
            }

            _context.Games.Remove(game);
      
            _context.SaveChanges();
            return true;

        }
        //Add a new deck to the game
        public void AddNewDeck(Deck d,int game_id)
        {
            Game g = _context.Games.Where(game => game.game_id == game_id).Single();
            Debug.WriteLine(g.game_id);
            g.decks.Add(d);
            _context.SaveChanges();

        }
        //Add new players to the game
        public Game AddPlayers(int game_id,int player_id)
        {
            Game g = _context.Games.Where(game => game.game_id == game_id).Single();
            Player p = _context.Players.Where(Player => Player.player_id == player_id).Single();
            g.players.Add(p);
            _context.SaveChanges();
            return g;
        }

        //Remove players from the game
        public Game RemovePlayers(int game_id, int player_id)
        {
            Game g = _context.Games.Where(game => game.game_id == game_id).Single();
            Player p = _context.Players.Where(Player => Player.player_id == player_id).Single();
            g.players.Remove(p);
            _context.SaveChanges();
            return g;
        }

        //Deal cards to players
        public Boolean DealCard(int game_id, int player_id)
        {
           
            Game g = _context.Games.Where(game => game.game_id == game_id).Single();
            Player p = _context.Players.Where(Player => Player.player_id == player_id).Single();

            List<Card> cardstodraw = (from cards in _context.Cards
                                      join deck in _context.Decks on cards.deck_id equals deck.deck_id
                                      join game in _context.Games on deck.game_id equals game.game_id
                                      where cards.player_id == null
                                      orderby cards.deck_id ascending
                                      select cards).ToList();


            if(cardstodraw.Count == 0)
            {
                return false;
            }
            else
            {
                cardstodraw.First().player_id = player_id;
                _context.SaveChanges();
            }

            return true;

        }

        //Shuffles the deck 
        //returns void
        public void Shuffle(int game_id)
        {

            Game g = _context.Games.Where(game => game.game_id == game_id).Single();
            List<Deck> d = _context.Decks.Where(deck => deck.game_id == game_id).ToList();
            DeckHelper dh = new DeckHelper();


            for (int i = 0; i < d.Count; i++)
            {
                List<Card> Cards = _context.Cards.Where(card => card.deck_id == d[i].deck_id).ToList();
                List<Card> ShuffledCards = dh.ShuffleCards(Cards);
                Cards = ShuffledCards;
            }
            _context.SaveChanges();


        }

        //Responsible for getting undealt cards e.g: 3 spades,2 hearts remaining 
        public IEnumerable<UndealtLineObject> GetUndealtCards(int game_id)
        {
            var cards = from game in _context.Games
                        join deck in _context.Decks on game.game_id equals deck.game_id
                        join card in _context.Cards on deck.deck_id equals card.deck_id
                        where card.player_id == null
                        group card by card.suit into cardgroup
                        select new UndealtLineObject
                        {
                            suit = cardgroup.Key.ToString(),
                            count = cardgroup.Count()
                        };
            return cards;
        }

        /*Responsible for getting count of undealt cards based on suit and value sorted by suit and
        face type*/
        public IEnumerable<UndealtLineObjectsValue> GetUndealtCardsByValue(int game_id)
        {
            var cards = from game in _context.Games
                        join deck in _context.Decks on game.game_id equals deck.game_id
                        join card in _context.Cards on deck.deck_id equals card.deck_id
                        where card.player_id == null
                        group card by new { card.suit, card.value } into cardgroup
                        orderby cardgroup.Key.value descending
                        select new UndealtLineObjectsValue
                        {

                            suit = cardgroup.Key.suit.ToString(),
                            count = cardgroup.Count(),
                            value = cardgroup.Key.value,

                        };
            return cards;
        }





    }
}
