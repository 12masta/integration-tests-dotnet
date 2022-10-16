using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntegrationTestsApi;
using IntegrationTestsApi.Models;

namespace IntegrationTestsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastDatabase : ControllerBase
    {
        private readonly WeatherForecastContext _context;

        public WeatherForecastDatabase(WeatherForecastContext context)
        {
            _context = context;
        }

        // GET: api/WeatherForecastDatabase
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetWeatherForecasts()
        {
            if (_context.WeatherForecasts == null)
            {
                return NotFound();
            }

            return await _context.WeatherForecasts.ToListAsync();
        }

        // GET: api/WeatherForecastDatabase/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherForecast>> GetWeatherForecast(long id)
        {
            if (_context.WeatherForecasts == null)
            {
                return NotFound();
            }

            var weatherForecast = await _context.WeatherForecasts.FindAsync(id);

            if (weatherForecast == null)
            {
                return NotFound();
            }

            return weatherForecast;
        }

        // PUT: api/WeatherForecastDatabase/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeatherForecast(long id, WeatherForecast weatherForecast)
        {
            if (id != weatherForecast.Id)
            {
                return BadRequest();
            }

            _context.Entry(weatherForecast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherForecastExists(id))
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

        // POST: api/WeatherForecastDatabase
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WeatherForecast>> PostWeatherForecast(WeatherForecast weatherForecast)
        {
            if (_context.WeatherForecasts == null)
            {
                return Problem("Entity set 'WeatherForecastContext.WeatherForecasts'  is null.");
            }

            _context.WeatherForecasts.Add(weatherForecast);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeatherForecast", new {id = weatherForecast.Id}, weatherForecast);
        }

        // DELETE: api/WeatherForecastDatabase/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeatherForecast(long id)
        {
            if (_context.WeatherForecasts == null)
            {
                return NotFound();
            }

            var weatherForecast = await _context.WeatherForecasts.FindAsync(id);
            if (weatherForecast == null)
            {
                return NotFound();
            }

            _context.WeatherForecasts.Remove(weatherForecast);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeatherForecastExists(long id)
        {
            return (_context.WeatherForecasts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}