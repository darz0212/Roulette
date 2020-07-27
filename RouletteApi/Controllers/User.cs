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
    public class User : Controller
    {
        private UserRouletteCollection db = new UserRouletteCollection();

        [HttpGet]
        public IActionResult GetAllRoulletes()
        {
            return Ok(db.GetAllUsers());
        }

        [HttpGet("{id}")]
        public IActionResult GetRoulleteById(string id)
        {
            var result = db.GetUsersById(id);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateRoulette([FromBody] UserRoulette user)
        {
            if (user == null)
                return BadRequest();

            db.InsertUsers(user);

            return Created("Created", true);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRoulette(string id, [FromBody] UserRoulette user)
        {
            if (user == null)
                return BadRequest();

            db.UpdateUsers(user, id);

            return Created("Created", true);
        }
    }
}
