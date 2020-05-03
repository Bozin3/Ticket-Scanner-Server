using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketScannerBackend.Data;
using TicketScannerBackend.Models;

namespace TicketScannerBackend.Controllers
{
    [Authorize]
    [Route("api/{eventId}/[controller]")]
    public class TicketController : ControllerBase
    {
        private ITicketServices ticketServices;
        public TicketController(ITicketServices ticketServices)
        {
            this.ticketServices = ticketServices;
        }
        // GET api/ticket
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string type, int eventId)
        {
            // if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
            //     return Unauthorized();
            // }

            var ticketsResponse = new TicketResponse<List<Tickets>>()
            {
                Error = true // default
            };

            if (type == null)
            {
                ticketsResponse.Message = "Please provide which type of tickets you want.";
                return BadRequest(ticketsResponse);
            }

            try
            {
                List<Tickets> tickets = null;

                switch (type)
                {
                    case "active":
                        tickets = await ticketServices.GetActiveTickets(eventId);
                        break;
                    case "inactive":
                        tickets = await ticketServices.GetNonActiveTickets(eventId);
                        break;
                    case "all":
                        tickets = await ticketServices.GetAllTickets(eventId);
                        break;    
                    default:
                        break;
                }

                ticketsResponse.Error = false;
                ticketsResponse.Message = "Valid";
                ticketsResponse.ticket = tickets;

                return Ok(ticketsResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ticketsResponse.Error = true;
                ticketsResponse.Message = e.Message;
                return StatusCode(500, ticketsResponse);
            }

        }

        [HttpGet("{barcode}")]
        public IActionResult CheckTicket(string barcode, int eventId)
        {
            Console.WriteLine(barcode);
            var ticketsResponse = new TicketResponse<Tickets>()
            {
                Error = true // default
            };

            if (barcode == null)
            {
                ticketsResponse.Message = "Barcode null";
                return BadRequest(ticketsResponse);
            }

            try
            {
                var ticket = ticketServices.GetTicket(barcode, eventId);
                if (ticket == null)
                {
                    return NotFound($"Ticket not found, barcode {barcode}");
                }

                ticketsResponse.Error = false;
                ticketsResponse.Message = "Valid";
                ticketsResponse.ticket = CreateResponseTicket(ticket);
                if (!ticket.IsActivated)
                {
                    if (ticketServices.UpdateTicket(ticket))
                    {
                        Console.WriteLine("Updated");
                        return Ok(ticketsResponse);
                    }
                    else
                    {
                        ticketsResponse.Error = true;
                        ticketsResponse.Message = "Error while updating the ticket";
                        return StatusCode(500, ticketsResponse);
                    }
                }else{
                    // ticket is already activated, no need for updating
                    return Ok(ticketsResponse);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ticketsResponse.Error = true;
                ticketsResponse.Message = e.Message;
                return StatusCode(500, ticketsResponse);
            }

        }

        private Tickets CreateResponseTicket(Tickets ticket)
        {
            return new Tickets()
            {
                Id = ticket.Id,
                Barcode = ticket.Barcode,
                IsActivated = ticket.IsActivated,
                ActivatedAt = ticket.ActivatedAt
            };
        }
    }
}