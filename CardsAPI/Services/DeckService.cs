using CardsAPI.Context;
using CardsAPI.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardsAPI.Services
{
    //Deck Service
    //Responsible for Deck related tasks
    public class DeckService
    {
        CardsContext _context;
        public DeckService(CardsContext context)
        {
            _context = context;

        }
        //Creation of deck
        public Deck MakeDeck(Deck d)
        {
            _context.Decks.Add(d);
            _context.SaveChanges();
            return d;
        }
        //Return all decks
            public ICollection<Deck> GetAllDecks()
            {
                var decks = _context.Decks.ToList();
                return decks;
            }

        public ICollection<Deck> GetDeckbyGameID(int game_id)
        {
            var decks = _context.Decks.Where(d => d.game_id == game_id).ToList();
            return decks;
        }





    }
}
