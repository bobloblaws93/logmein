using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CardsAPI.models
{
    public class Player
    {
        //player model
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int player_id { get; set; }
        public string Name { get; set; }
        public int value { get; set; }
        public Game game { get; set; }
        public int? game_id { get; set; }
        public ICollection<Card> cards { get; set; }

    }
}
