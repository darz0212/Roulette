using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RouletteApi.Models;
using RouletteApi.Repositories;

namespace RouletteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetController : Controller
    {
        private BetCollection db = new BetCollection();
        private RouletteCollection dbRoulette = new RouletteCollection();
        private UserRouletteCollection dbUser = new UserRouletteCollection();

        [HttpGet]
        public IActionResult GetAllBets()
        {
            return Ok(db.GetAllBets());
        }

        [HttpGet("{id}")]
        public IActionResult GetBetById(string id)
        {
            var result = db.GetBetById(id);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateBet([FromBody] Bet bet)
        {
            if (bet == null)
                return BadRequest();

            if(string.IsNullOrEmpty(bet.idUser) || string.IsNullOrEmpty(bet.idRoulette))
                return BadRequest("User id or Roulette id is not valid");

            var resultRoulette = dbRoulette.GetRouletteById(bet.idRoulette, true);
            var resultUser = dbUser.GetUsersById(bet.idUser);

            if (resultUser == null)
                return BadRequest("User not found");

            if (resultRoulette == null)
                return BadRequest("Roulette not found or is not active");           

            if (bet.moneyValue < 0 || bet.moneyValue >10000)
                return BadRequest("Bet value is not valid");

            if(bet.moneyValue > resultUser.credit)
                return BadRequest("The user's credit can not support the bet");

            if ((bet.betNumber < 0 && bet.betNumber > 36) || !(bet.betColor == "red" || bet.betColor == "black"))
                return BadRequest("The bet option is not valid");

            db.InsertBet(bet);

            var betUser = dbUser.GetUsersById(bet.idUser);
            betUser.credit -= bet.moneyValue;
            dbUser.UpdateUsers(betUser, betUser.id);

            return Created("Created", bet);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBet(string id, [FromBody] Bet bet)
        {
            if (bet == null)
                return BadRequest();

            db.UpdateBet(bet, id);

            return Created("Created", bet);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBet(string id)
        {
            db.DeleteBet(id);
            return NoContent();
        }
    }
}
