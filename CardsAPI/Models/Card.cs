using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CardsAPI.models
{

    //card model
    //enum of suit
    public enum suit { Heart,Diamond,Spades,Clubs}
    public enum face { Jack,Queen,King,Ace,Regular}
    public class Card
    {

        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int card_id { get; set; }
        public int value { get; set; }
        public int position { get; set; }

        public suit suit { get; set; }
        public face face { get; set; }
        public int? player_id { get; set; }
        public Player player { get; set; }

        public int deck_id { get; set; }
        public Deck deck { get; set; }






    }
}
