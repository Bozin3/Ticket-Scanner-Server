using System;
using System.Collections.Generic;

namespace TicketScannerBackend.Models
{
    public class Events
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public ICollection<Tickets> Tickets {get;set;} 
    }
}