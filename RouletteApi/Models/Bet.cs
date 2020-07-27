using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteApi.Models
{
    public class Bet
    {
        public int idBet { get; set; }
        public int moneyValue { get; set; }
        public int betNumber { get; set; }
        public string betColor { get; set; }
    }
}
