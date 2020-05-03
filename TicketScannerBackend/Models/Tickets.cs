using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketScannerBackend.Models
{
    public class Tickets
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public DateTime? ActivatedAt { get; set; }
        public bool IsActivated{ get; set; }
        public Events Event { get; set; }
        public int EventId { get; set; }
    }
}
