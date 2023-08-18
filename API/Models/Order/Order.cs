
namespace API.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderDescription { get; set; }
        public decimal OrderAmount { get; set; }
        public int OrderStatus { get; set; }
        public int ClientId { get; set; }
    }
}
