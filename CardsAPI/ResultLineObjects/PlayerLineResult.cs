using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardsAPI.ResultLineObjects
{
    //playerline result
    //responsible for returning the player and the value of their hand
    public class PlayerLineResult
    {
        public int player_id { get; set; }
        public int value { get; set; }

    }
}
