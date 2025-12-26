using Order.Domain.Models;

namespace Order.Application.Queries.GetOrderById
{
    public class OrderWithServicesDto
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public List<OrderServiceEntity> Services { get; set; } = new();
    }

}
