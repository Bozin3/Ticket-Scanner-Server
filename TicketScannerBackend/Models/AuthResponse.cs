namespace TicketScannerBackend.Models
{
    public class AuthResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public Clients Client { get; set; }
        public string Token { get; set; }
    }
}