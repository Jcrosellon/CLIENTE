namespace CLIENTE.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int Cliente_id { get; set; } // Debe coincidir con el nombre de la columna en la base de datos
        public Cliente Cliente { get; set; } = new Cliente(); // Propiedad de navegación hacia Cliente
        public DateTime Date { get; set; }
        public DateTime? StatusDate { get; set; }
        public DateTime? PreparingDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public decimal Total { get; set; }
    }
}
