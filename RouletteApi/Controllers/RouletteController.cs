using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using RouletteApi.Models;
using RouletteApi.Repositories;

namespace RouletteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : Controller
    {
        private RouletteCollection db = new RouletteCollection();
        private BetCollection dbBet = new BetCollection();
        private UserRouletteCollection dbUser = new UserRouletteCollection();

        [HttpGet]
        public IActionResult GetAllRoulletes()
        {
            return Ok(db.GetAllRoulettes());
        }

        [HttpGet("{id}")]
        public IActionResult GetRoulleteById(string id)
        {
            var result = db.GetRouletteById(id);
            if(result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateRoulette([FromBody] Roulette roulette)
        {
            if (roulette == null)
                return BadRequest();

            db.InsertRoulette(roulette);

            return Created("Created", true);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRoulette(string id, [FromBody] Roulette roulette)
        {
            if (roulette == null)
                return BadRequest();

            db.UpdateRoulette(roulette, id);

            return Created("Created", roulette);
        }

        [HttpPost("{id}")]
        public IActionResult CloseRouletteBets(string id)
        {
            var seed = Environment.TickCount;
            var random = new Random(seed);
            var roulette = db.GetRouletteById(id);
            var bets = dbBet.GetAllBets(id);
            var NumberRandomResult = random.Next(0, 36);
            var colorRandomResult = random.Next(0, 1);

            foreach (var bet in bets)
            {
                if(bet.betNumber == NumberRandomResult && string.IsNullOrEmpty(bet.betColor))
                    bet.isWinner = true;

                if (bet.betColor == "red" && colorRandomResult == 0)
                    bet.isWinner = true;

                if (bet.betColor == "black" && colorRandomResult == 1)
                    bet.isWinner = true;

                if (bet.isWinner)
                {
                    var betUser = dbUser.GetUsersById(bet.idUser);
                    if(betUser != null)
                    {
                        betUser.credit += bet.moneyValue * 2;
                        dbUser.UpdateUsers(betUser, betUser.id);
                        dbBet.UpdateBet(bet, bet.id);
                    }                    
                }
            }
            roulette.state = "close";
            db.UpdateRoulette(roulette, roulette.id);

            return Ok(bets);
        }
    }
}
