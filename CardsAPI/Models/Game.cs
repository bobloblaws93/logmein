using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CardsAPI.models
{
    //game object model
    public class Game
    {

        public Game()
        {
            players = new List<Player>();
            decks = new List<Deck>();

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int game_id { get; set; }
        public ICollection<Player> players { get; set; }
        public ICollection<Deck> decks{ get; set; }

    }
}
