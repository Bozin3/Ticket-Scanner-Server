using System.Collections.Generic;
using System.Threading.Tasks;
using TicketScannerBackend.Models;

namespace TicketScannerBackend.Data
{
    public interface IEventServices
    {
        Task<List<Events>> GetEvents(string type);
    }
}