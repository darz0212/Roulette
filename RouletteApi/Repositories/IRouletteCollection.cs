using RouletteApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteApi.Repositories
{
    public interface IRouletteCollection
    {
        Task InsertRoulette(Roulette product);
        Task UpdateRoulette(Roulette product, string id);
        Task<List<Roulette>> GetAllRoulettes();
        Task<Roulette> GetRouletteById (string id);
    }
}
