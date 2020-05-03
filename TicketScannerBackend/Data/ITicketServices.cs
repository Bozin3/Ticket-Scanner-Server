using System.Collections.Generic;
using System.Threading.Tasks;
using TicketScannerBackend.Models;

namespace TicketScannerBackend.Data
{
    public interface ITicketServices
    {
        Tickets GetTicket(string barcode, int eventId);

        Task<List<Tickets>> GetAllTickets(int eventId);
        Task<List<Tickets>> GetActiveTickets(int eventId);
        Task<List<Tickets>> GetNonActiveTickets(int eventId);
        bool UpdateTicket(Tickets ticket);
    }
}