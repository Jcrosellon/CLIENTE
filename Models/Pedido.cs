namespace CLIENTE.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = new Cliente(); // Inicializado para evitar advertencias
        public DateTime Date { get; set; }
        public DateTime? StatusDate { get; set; }
        public DateTime? PreparingDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public decimal Total { get; set; }
    }
}
