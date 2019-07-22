using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CardsAPI.Context;
using CardsAPI.Helpers;
using CardsAPI.models;
using CardsAPI.ResultLineObjects;
using CardsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CardsAPI.Controllers
{
    //Endpoints associated with game related tasks
    [Route("api/[controller]/[action]")]
    public class GameController : ControllerBase
    {

        private readonly CardsContext db;
        //intialize game context
        public GameController(CardsContext db)
        {
            this.db = db;
        }       
        //creation of game
        [HttpGet]
        [ActionName("CreateGame")]
        public async Task<ActionResult<Game>> Game() {
            Game game = new Game();
            GameService gs = new GameService(db);

            Game CreatedGame = gs.CreateNewGame(game);
            return CreatedGame;
        }

        [HttpDelete]
        [ActionName("DeleteGame")]
        public String DeleteGame(int game_id)
        {
            Game game = new Game();
            GameService gs = new GameService(db);

            if (gs.DeleteGame(game_id))
                return ("Game deleted");
            else
                return ("Can't Delete Game");
        }
        //addition of a player to the game
        [HttpPut]
        [ActionName("AddPlayer")]
        public async Task<ActionResult<Game>> AddPlayer( int player_id, int game_id)
        {
            Game game = new Game();
            GameService gs = new GameService(db);
            Game CreatedGame = gs.AddPlayers(game_id, player_id);
            return CreatedGame;
        }
        //addition of deck to game
        [HttpPost]
        [ActionName("AddDeck")]
        public async Task<ActionResult<Deck>> AddDeck(int game_id)
        {
            GameService gs = new GameService(db);
            DeckHelper dh = new DeckHelper();
            Deck d = dh.CreateRandomDeck();
            gs.AddNewDeck(d, game_id);
            return d;
        }

        //responsible for shuffling the deck
        //takes a game_id paramter
        [HttpGet("{game_id}")]
        [ActionName("Shuffle")]
        public String Shuffle(int game_id)
        {
            GameService gs = new GameService(db);
            DeckHelper dh = new DeckHelper();
            gs.Shuffle(game_id);
            return "Deck is Shuffled";
        }

        //Deal cards endpoint responsible for dealing a card
        //Takes a player id and game id
        [HttpPost]
        [ActionName("DealCards")]
        public ICollection<Card> dealCard(int player_id, int game_id)
        {
            GameService gs = new GameService(db);
            PlayerService ps = new PlayerService(db);

            bool dealcardsucess = gs.DealCard(game_id,player_id);
            if (dealcardsucess)
            {
                ICollection<Card> playercards = ps.GetPlayerCards(player_id);
                return playercards;
            }

            return null;
        }

        //end point responsible for getting undealt cards and showing how many cards are left by suit
        //takes a game_id parameter
        [HttpGet("{game_id}")]
        [ActionName("GetUndealtCards")]
        public IEnumerable<UndealtLineObject> GetUndealtCards(int game_id)
        {
            GameService gs = new GameService(db);
            IEnumerable<UndealtLineObject> ulo = gs.GetUndealtCards(game_id);
            return ulo;
        }

        //endpoint responsible for getting undealt cards and sorting them by suit and value
        //takes game_id
        [HttpGet("{game_id}")]
        [ActionName("GetUndealtCardsByValue")]
        public IEnumerable<UndealtLineObjectsValue> GetUndealtCardsByValue(int game_id)
        {
            GameService gs = new GameService(db);
            IEnumerable<UndealtLineObjectsValue> ulov = gs.GetUndealtCardsByValue(game_id);
            return ulov;
        }

      

    }
}