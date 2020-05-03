using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketScannerBackend.Models;

namespace TicketScannerBackend.Data
{
    public class EventServices : IEventServices
    {
        private BazaContext db;
        public EventServices(BazaContext db){
            this.db = db;
        }
        public async Task<List<Events>> GetEvents(string type)
        {
            var events = db.Events.AsQueryable();

            if(type.Equals("upcoming")){
                events = events.Where(e => e.EventDate.Date >= DateTime.Today);
            }else if (type.Equals("finished")){
                events = events.Where(e => e.EventDate.Date < DateTime.Today);
            }

            return await events.ToListAsync();
        }
    }
}