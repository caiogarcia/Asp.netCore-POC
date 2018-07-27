using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mywebapi.Controllers
{
    
    [Produces("application/json")]
    [Route("api/Ticket")]
    public class TicketController : Controller
    {

        private TicketContext _context;

        public TicketController(TicketContext context)
        {
            _context = context;

            if(_context.TicketItems.Count() == 0)
            {
                _context.TicketItems.Add(new TicketItem { Consert = "Beyoncé" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<TicketItem> GetAll()
        {
            return _context.TicketItems.AsNoTracking().ToList();
        }

        [HttpGet("{id}", Name = "GetTicket")]
        public IActionResult GetById(long id)
        {
            var ticket = _context.TicketItems.FirstOrDefault(t => t.Id == id);
            if(ticket == null)
            {
                return NotFound();
            }
            return new ObjectResult(ticket);
        }

        [HttpPost]
        public IActionResult Create([FromBody]TicketItem ticket)
        {
            if(ticket == null)
            {
                return BadRequest();
            }
            _context.TicketItems.Add(ticket);
            _context.SaveChanges();

            return CreatedAtRoute("GetTicket", new {id = ticket.Id }, ticket);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TicketItem ticket)
        {
            
            if(ticket == null || ticket.Id != id)
            {
                return BadRequest();
            }
            var tic = _context.TicketItems.FirstOrDefault(t => t.Id == id);
            if(tic == null)
            {
                return NotFound();
            }

            tic.Artist = ticket.Artist;
            tic.Consert = ticket.Consert;
            tic.Avaliable = ticket.Avaliable;

            _context.TicketItems.Update(tic);
            _context.SaveChanges();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var tic = _context.TicketItems.FirstOrDefault(t => t.Id == id);
            if(tic == null)
            {
                return NotFound();
            }

            _context.TicketItems.Remove(tic);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}