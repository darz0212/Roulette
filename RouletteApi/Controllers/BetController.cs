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
        public IActionResult CreateRoulette([FromBody] Bet bet)
        {
            if (bet == null)
                return BadRequest();

            db.InsertBet(bet);

            return Created("Created", true);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRoulette(string id, [FromBody] Bet bet)
        {
            if (bet == null)
                return BadRequest();

            db.UpdateBet(bet, id);

            return Created("Created", true);
        }
    }
}
