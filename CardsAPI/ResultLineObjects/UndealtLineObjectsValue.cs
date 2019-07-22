using CardsAPI.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardsAPI.ResultLineObjects
{
    //model dealing with undealt cards based on value and suit
    public class UndealtLineObjectsValue
    {
        public string suit { get; set; }
        public int count { get; set; }
        public int value { get; set; }

    }
}
