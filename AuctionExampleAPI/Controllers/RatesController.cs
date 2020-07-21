﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuctionExampleAPI.Data;
using AuctionExampleAPI.Models;

namespace AuctionExampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly AuctionExampleContext _context;

        public RatesController(AuctionExampleContext context)
        {
            _context = context;
        }

        // GET: api/rates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rate>>> GetRate()
        {
            return await _context.Rate.ToListAsync();
        }

        // GET: api/rates/item/5/
        [HttpGet("item/{id:int}")]
        public async Task<ActionResult<IEnumerable<Rate>>> GetRatesByItem(int id)
        {
            return await _context.Rate.Where(r => r.ItemId == id).OrderByDescending(r => r.RateTime).ToListAsync();
        }

        // GET: api/rates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rate>> GetRate(int id)
        {
            var rate = await _context.Rate.FindAsync(id);

            if (rate == null)
            {
                return NotFound();
            }

            return rate;
        }

        // PUT: api/rates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRate(int id, Rate rate)
        {
            if (id != rate.RateId)
            {
                return BadRequest();
            }

            _context.Entry(rate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/rates
        [HttpPost]
        public async Task<ActionResult<Rate>> PostRate(Rate rate)
        {
            await _context.Rate.AddAsync(rate);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRate), new { id = rate.RateId }, rate);
        }

        // DELETE: api/rates/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Rate>> DeleteRate(int id)
        {
            var rate = await _context.Rate.FindAsync(id);
            if (rate == null)
            {
                return NotFound();
            }

            _context.Rate.Remove(rate);
            await _context.SaveChangesAsync();

            return rate;
        }

        private bool RateExists(int id)
        {
            return _context.Rate.Any(e => e.RateId == id);
        }
    }
}
