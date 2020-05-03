using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketScannerBackend.Models;

namespace TicketScannerBackend.Data
{
    public class AuthServices : IAuthServices
    {
        private BazaContext db;
        public AuthServices(BazaContext db){
            this.db = db;
        }

        public async Task<Clients> ClientAuth(ClientCreds clientCreds)
        {
            var client = await db.Clients.FirstOrDefaultAsync(c => c.Username == clientCreds.ClientName);
            if(client != null){
                if(client.Password.Equals(clientCreds.Password)){
                    return client;
                }
            }
            return null;
        }
    }
}