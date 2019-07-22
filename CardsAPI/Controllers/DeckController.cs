using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CardsAPI.models;
using CardsAPI.Helpers;
using System.Diagnostics;
using CardsAPI.Context;
using CardsAPI.Services;

namespace CardsAPI.Controllers
{
    //Deck Controller
    //Endpoints related to the Deck
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeckController : ControllerBase
    {
        private readonly CardsContext db;
        //intialize context
        public DeckController(CardsContext db)
        {
            this.db = db;
        }

        public ActionResult<IEnumerable<string>> Get()
        {
          

            return new string[] { "value41", "value2" };
        }
        //responsible for creation of deck
        [HttpGet]
        [ActionName("CreateDeck")]
        public async Task<ActionResult<Deck>> CreateDeck()
        {
            DeckHelper dh = new DeckHelper();
            Deck d = dh.CreateRandomDeck();
            DeckService ds = new DeckService(db);
            Deck madeDeck = ds.MakeDeck(d);
            return madeDeck;
        }




    }
}