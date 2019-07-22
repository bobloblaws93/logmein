using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardsAPI.Context;
using CardsAPI.models;
using CardsAPI.ResultLineObjects;
using CardsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CardsAPI.Controllers
{
    //Endpoint responsible for player related services
    [Route("api/[controller]/[action]")]
    public class PlayersController : ControllerBase
    {
        
        private readonly CardsContext db;

        public PlayersController(CardsContext db)
        {
            this.db = db;
        }

        //Endpoint responsible for player creation
        [HttpPut]
        [ActionName("CreatePlayer")]
        public async Task<ActionResult<Player>> Player()
        {
            Player player = new Player();
            PlayerService ps = new PlayerService(db);

            Player p = ps.CreateNewPlayer(player);
            return p;
        }

        [HttpDelete]
        [ActionName("DeletePlayer")]
        public String DeletePlayer(int player_id)
        {
            Player player = new Player();
            PlayerService ps = new PlayerService(db);

            if (ps.DeletePlayer(player_id))
                return "Deleted";
            else
                return "Unable to Delete";
        }


        //Endpoint responsible for getting player hands and returning the total value
        //takes in a player id and game id
        [HttpGet("{player_id}/{game_id}")]
        [ActionName("GetPlayerCardsByValue")]
        public IEnumerable<PlayerLineResult> GetPlayerCardsByValue(int player_id,int game_id)
        {
            Player player = new Player();
            PlayerService ps = new PlayerService(db);
            IEnumerable<PlayerLineResult> plr = ps.GetPlayerCardsByValue(player_id, game_id);
            return plr;
        }

        //Endpoint responsible for getting player hands
        //takes in a player id and game id
        [HttpGet("{player_id}")]
        [ActionName("GetPlayerCards")]
        public ICollection<Card> GetPlayerCards(int player_id, int game_id)
        {
            Player player = new Player();
            PlayerService ps = new PlayerService(db);
            ICollection<Card> cards = ps.GetPlayerCards(player_id);
            return cards;
        }
    }
}