using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Model.ViewModel;

namespace WebShop.Model.Models
{
    public class OrderDetailsAddModel
    {
        public int OrderId { get; set; }
        public IEnumerable<ShoppingCart> CartList { get; set; }
    }
}
