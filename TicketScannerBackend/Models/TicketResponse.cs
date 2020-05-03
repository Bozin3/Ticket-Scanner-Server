namespace TicketScannerBackend.Models
{
    public class TicketResponse<T>
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public T ticket { get; set; }
    }
}