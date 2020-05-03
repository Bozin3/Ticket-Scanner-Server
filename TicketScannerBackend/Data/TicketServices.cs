using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketScannerBackend.Models;

namespace TicketScannerBackend.Data
{
    public class TicketServices : ITicketServices
    {
        private BazaContext db;
        public TicketServices(BazaContext db){
            this.db = db;
        }
        public Tickets GetTicket(string barcode, int eventId)
        {
            var ticket = db.Tickets.FirstOrDefault(t => t.Barcode.Equals(barcode) && t.EventId == eventId);
            return ticket;
        }

        public async Task<List<Tickets>> GetActiveTickets(int eventId)
        {
            return await db.Tickets.Where(t => t.IsActivated == true && t.EventId == eventId).ToListAsync();
        }

        public async Task<List<Tickets>> GetNonActiveTickets(int eventId)
        {
            return await db.Tickets.Where(t => t.IsActivated == false && t.EventId == eventId).ToListAsync();
        }

        public bool UpdateTicket(Tickets ticket)
        {
            ticket.ActivatedAt = DateTime.Now;
            ticket.IsActivated = true;
            return db.SaveChanges() > 0;
        }

        public async Task<List<Tickets>> GetAllTickets(int eventId)
        {
            return await db.Tickets.Where(t => t.EventId == eventId).ToListAsync();
        }
    }
}