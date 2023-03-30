using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Model.Models
{
    public class UpdateStatusModel
    {
        public int OrderHeaderId { get; set; }
        public string OrderStatus { get; set;}

        public string? PaymentStatus { get; set; }
    }
}
