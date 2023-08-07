using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Domain.src.Entities
{
    public class Order : BaseEntity
    {
        public User User { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }

        public enum OrderStatus
        {
            Processing,
            Shipped
        }
    }
}