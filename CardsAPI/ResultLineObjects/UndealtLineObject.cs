using CardsAPI.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardsAPI.ResultLineObjects
{
    //model based on undealt cards based on suit and count
    public class UndealtLineObject
    {
        public string suit { get; set; }
        public int count { get; set; }

    }
}
