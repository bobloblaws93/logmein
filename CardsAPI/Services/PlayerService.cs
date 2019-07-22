using CardsAPI.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardsAPI.models;
using CardsAPI.ResultLineObjects;

namespace CardsAPI.Services
{
    //Player Service
    //Responsible for Player related services
    public class PlayerService
    {

        CardsContext _context;
        public PlayerService(CardsContext context)
        {
            _context = context;

        }
        //Return all players
        public ICollection<Player> GetPlayers()
        {
            var players = _context.Players.ToList();
            return players;
        }

        public Player GetPlayersById(int player_id)
        {
            var player = _context.Players.Where(p => p.player_id == player_id).SingleOrDefault(); ;
            return player;
        }


        //Return a players hands of cards
        public ICollection<Card> GetPlayerCards(int player_id)
        {
            var cards = _context.Cards.Where(c => c.player_id == player_id).ToList();
            return cards;
        }
        //creation of a new player
        public Player CreateNewPlayer(Player p)
        {
            _context.Players.Add(p);
            _context.SaveChanges();
            return p;

        }
        //Delete player
        public bool DeletePlayer(int player_id)
        {
            Player player = _context.Players.Where(p => p.player_id == player_id).FirstOrDefault();

            List<Card> cards = _context.Cards.Where(c => c.player_id == player_id).ToList();

            //remove player cards
            foreach (Card c in cards)
            {
                player.cards.Remove(c);
            }
            int? gameid = player.game_id;

            if (gameid == null)
                return true;
            else
            {
                Game game = _context.Games.Where(g => g.game_id == gameid).FirstOrDefault();
                game.players.Remove(player);
                _context.SaveChanges();
                return true;
            }


        }


        //Get value of cards of a players hands
        public IEnumerable<PlayerLineResult> GetPlayerCardsByValue(int player_id,int game_id)
        {
            var cards = from c in _context.Cards
                        join player in _context.Players on c.player_id equals player.player_id
                        group c by player.player_id into playercardgroup
                        select new PlayerLineResult
                        {
                            player_id = playercardgroup.Key,
                            value = playercardgroup.Sum(c => c.value)
                        };
            return cards;
        }





    }
}
