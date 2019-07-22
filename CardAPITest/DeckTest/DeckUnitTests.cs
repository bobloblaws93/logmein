using CardsAPI.Context;
using CardsAPI.Helpers;
using CardsAPI.models;
using CardsAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CardAPITest.DeckTest
{
    public class DeckUnitTests
    {

        CardsContext _context;

        public DeckUnitTests()
        {

                var options = new DbContextOptionsBuilder<CardsContext>()
                    .UseInMemoryDatabase(databaseName: "CardsDBtest1")
                    .Options;
                _context = new CardsContext(options);

        }
        [Fact]
        public void TestDeckCreation()
        {

                Deck d = new Deck();
                var service = new DeckService(_context);
                Deck createdDeck = service.MakeDeck(d);
                Assert.Equal(1, createdDeck.deck_id);
        }

        [Fact]
        public void TestDeckAddition()
        {
            DeckHelper dh = new DeckHelper();
            Deck d1 = dh.CreateRandomDeck();
            Deck d2 = dh.CreateRandomDeck();

            var deckservice = new DeckService(_context);
            Deck createdDeck1 = deckservice.MakeDeck(d1);
            Deck createdDeck2 = deckservice.MakeDeck(d2);

            Game game = new Game();
            var gameservice = new GameService(_context);
            Game newgame = gameservice.CreateNewGame(game);

            gameservice.AddNewDeck(createdDeck1, newgame.game_id);
            gameservice.AddNewDeck(createdDeck2, newgame.game_id);

            ICollection<Deck> gamedecks = deckservice.GetDeckbyGameID(newgame.game_id);
            _context.Dispose();
            Assert.Equal(2, gamedecks.Count);

        }

    }
}
