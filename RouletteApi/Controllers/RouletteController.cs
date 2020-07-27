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
        private IRouletteCollection db = new RouletteCollection();

        [HttpGet]
        public async Task<IActionResult> GetAllRoulletes()
        {
            return Ok(await db.GetAllRoulettes());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoulleteById(string id)
        {
            return Ok(await db.GetRouletteById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoulette([FromBody] Roulette roulette)
        {
            if (roulette == null)
                return BadRequest();

            await db.InsertRoulette(roulette);

            return Created("Created", true);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoulette([FromBody] Roulette roulette, string id)
        {
            if (roulette == null)
                return BadRequest();

            await db.UpdateRoulette(roulette, id);

            return Created("Created", true);
        }
    }
}
