using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Model.Models
{
    public class UpdateStripePaymentModel
    {
      
        public int Id { get; set; }
        public string SessionId { get; set; }
        public string PaymentIntentId { get; set; }
    }
}
