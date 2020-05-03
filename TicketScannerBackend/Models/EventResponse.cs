using System.Collections.Generic;

namespace TicketScannerBackend.Models
{
    public class EventResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public List<Events> Events { get; set; } 
    }
}