﻿using System;
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

            return Created("Created", true);
        }
    }
}
