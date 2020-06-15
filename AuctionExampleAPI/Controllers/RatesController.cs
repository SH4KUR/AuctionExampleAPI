using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionExampleAPI.Data;
using AuctionExampleAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionExampleAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatesController : ControllerBase
    {
        private readonly AuctionExampleContext _context;

        public RatesController(AuctionExampleContext context)
        {
            _context = context;
        }

        // GET: api/rates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rate>>> Get()
        {
            return await _context.Rate
                .Include(i => i.Item)
                .ToListAsync();
        }

        // GET: api/rates/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Rate>> Get(int id)
        {
            var rate = await _context.Rate
                .Include(i => i.Item)
                .FirstOrDefaultAsync(i => i.RateId == id);

            if (rate == null)
                return NotFound();

            return rate;
        }
    }
}
