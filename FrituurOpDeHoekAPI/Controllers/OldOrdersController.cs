using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FrituurOpDeHoekAPI.Data;
using FrituurOpDeHoekAPI.Models;

namespace FrituurOpDeHoekAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OldOrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OldOrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/OldOrders
        /// <summary>
        /// This function retrieves all old orders from the database
        /// </summary>
        /// <returns> A list of old orders </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OldOrder>>> GetOldOrders()
        {
          if (_context.OldOrders == null)
          {
              return NotFound();
          }
            return await _context.OldOrders.ToListAsync();
        }

        // GET: api/OldOrders/5
        /// <summary>
        /// This function retrieves a specific old order from the database by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> A specific old order </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OldOrder>> GetOldOrder(int id)
        {
          if (_context.OldOrders == null)
          {
              return NotFound();
          }
            var oldOrder = await _context.OldOrders.FindAsync(id);

            if (oldOrder == null)
            {
                return NotFound();
            }

            return oldOrder;
        }

        // PUT: api/OldOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// This function allows for updating a specific old order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldOrder"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOldOrder(int id, OldOrder oldOrder)
        {
            if (id != oldOrder.Id)
            {
                return BadRequest();
            }

            _context.Entry(oldOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OldOrderExists(id))
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

        // POST: api/OldOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// This function allows for the creation of new old orders
        /// </summary>
        /// <param name="oldOrder"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<OldOrder>> PostOldOrder(OldOrder oldOrder)
        {
          if (_context.OldOrders == null)
          {
              return Problem("Entity set 'AppDbContext.OldOrders'  is null.");
          }
            _context.OldOrders.Add(oldOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOldOrder", new { id = oldOrder.Id }, oldOrder);
        }

        // DELETE: api/OldOrders/5
        /// <summary>
        /// This function allows for the deletion of a single old order by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOldOrder(int id)
        {
            if (_context.OldOrders == null)
            {
                return NotFound();
            }
            var oldOrder = await _context.OldOrders.FindAsync(id);
            if (oldOrder == null)
            {
                return NotFound();
            }

            _context.OldOrders.Remove(oldOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OldOrderExists(int id)
        {
            return (_context.OldOrders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
