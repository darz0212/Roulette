using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteApi.Models
{
    public class Roulette
    {
        public int idRoulette {get; set;}
        public string state { get; set; }
        public virtual List<Bet> bets { get; set; }
    }
}
