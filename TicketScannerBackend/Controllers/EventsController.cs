using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketScannerBackend.Data;
using TicketScannerBackend.Models;

namespace TicketScannerBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private IEventServices eventServices;
        public EventsController(IEventServices eventServices){
            this.eventServices = eventServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents([FromQuery] string type){
            EventResponse response = new EventResponse(){
                Error = true,
                Message = "Default error"
            };

            if(type == null){
                type = "";
            }

            try{
                var events = await eventServices.GetEvents(type);
                response.Error = false;
                response.Message = "Valid";
                response.Events = events;

                return Ok(response);
                   
            }catch(Exception ex){
                response.Error = true;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }
    }
}