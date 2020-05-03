using System.Threading.Tasks;
using TicketScannerBackend.Models;

namespace TicketScannerBackend.Data
{
    public interface IAuthServices
    {
        Task<Clients> ClientAuth(ClientCreds clientCreds);
    }
}