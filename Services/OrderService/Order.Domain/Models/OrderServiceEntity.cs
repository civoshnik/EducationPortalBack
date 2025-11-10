using System.ComponentModel.DataAnnotations;

namespace Order.Domain.Models
{
    public class OrderServiceEntity
    {
        [Key]
        public Guid OrderServiceId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ServiceId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
