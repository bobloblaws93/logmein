using CardsAPI.Context;
using CardsAPI.Helpers;
using CardsAPI.models;
using CardsAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CardAPITest.PlayerTest
{
    public class PlayerUnitTest
    {
        CardsContext _context;
        public PlayerUnitTest()
        {

            var options = new DbContextOptionsBuilder<CardsContext>()
                .UseInMemoryDatabase(databaseName: "CardsDBtest3")
                .Options;
            _context = new CardsContext(options);
        }

        [Fact]
     public void TestPlayerCreation()
        {

            Player p = new Player();
            p.Name = "test";
            var service = new PlayerService(_context);
            Player createdPlayer = service.CreateNewPlayer(p);
            Assert.Equal("test", createdPlayer.Name);
            // Run the test against one instance of the context
        }


        [Fact]
        public void TestPlayerDeletion()
        {
            Player p = new Player();
            var service = new PlayerService(_context);
            Player createdPlayer = service.CreateNewPlayer(p);

            bool playerdeleted = service.DeletePlayer(createdPlayer.player_id);
            Assert.True(playerdeleted);
        }


        [Fact]
        public void TestPlayerAddGame()
        {

            Player p = new Player();
            p.Name = "test";
            var service = new PlayerService(_context);
            Player createdPlayer = service.CreateNewPlayer(p);

            Game g = new Game();
            var gameservice = new GameService(_context);
            Game newgame = gameservice.CreateNewGame(g);
            gameservice.AddPlayers(g.game_id, createdPlayer.player_id);

            Assert.Equal(createdPlayer.game_id, g.game_id);


        }




        [Fact]
        public void TestPlayerDraw104()
        {
            Player p = new Player();
            var service = new PlayerService(_context);
            Player createdPlayer = service.CreateNewPlayer(p);

            Game g = new Game();
            var gameservice = new GameService(_context);
            Game newgame = gameservice.CreateNewGame(g);
            gameservice.AddPlayers(g.game_id, createdPlayer.player_id);

            DeckHelper dh = new DeckHelper();
            Deck d1 = dh.CreateRandomDeck();
            Deck d2 = dh.CreateRandomDeck();
            var deckservice = new DeckService(_context);
            Deck createdDeck1 = deckservice.MakeDeck(d1);
            Deck createdDeck2 = deckservice.MakeDeck(d2);

            gameservice.AddNewDeck(createdDeck1, g.game_id);
            gameservice.AddNewDeck(createdDeck2, g.game_id);


            for(int i = 0; i < 150; i++)
            {
                gameservice.DealCard(g.game_id, p.player_id);

            }

            ICollection<Card> cards = service.GetPlayerCards(p.player_id);


            Assert.Equal(104, cards.Count);



            // Run the test against one instance of the context
        }


       


    }
}
