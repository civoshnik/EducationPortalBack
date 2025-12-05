using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Models
{
    public class CartItemEntity
    {
        [Key]
        public Guid CartItemId { get; set; }
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
