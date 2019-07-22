using CardsAPI.Context;
using CardsAPI.models;
using CardsAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CardAPITest.GameTest
{
    public class GameUnitTest
    {

        CardsContext _context;
        public GameUnitTest()
        {

            var options = new DbContextOptionsBuilder<CardsContext>()
                .UseInMemoryDatabase(databaseName: "CardsDBtest2")
                .Options;
            _context = new CardsContext(options);
        }

        [Fact]
        public void TestAddGame()
        {
            Game g = new Game();
            var gameservice = new GameService(_context);
            Game newgame = gameservice.CreateNewGame(g);
            Assert.Equal(1,g.game_id);
        }


        [Fact]
        public void TestDeleteGame()
        {
            Game g = new Game();
            var gameservice = new GameService(_context);
            Game newgame = gameservice.CreateNewGame(g);
            var checkgamedeleted = gameservice.DeleteGame(newgame.game_id);
            Assert.True(checkgamedeleted);
        }



    }
}
