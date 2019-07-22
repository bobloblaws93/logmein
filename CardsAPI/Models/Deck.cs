using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CardsAPI.models
{
    //deck object model
    public class Deck
    {
        public Deck()
        {
            cards = new List<Card>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int deck_id { get; set; }
        public ICollection<Card> cards { get; set; }

        public Game game { get; set; }
        public int game_id { get; set; }

    }
}
