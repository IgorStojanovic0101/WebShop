using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Model.Models
{
    public class ShoppingCartSearchModel
    {
        public string ApplicationUserId { get; set; }
        public int ProductId { get; set; }

        public int IncrementCount { get; set; }
    }
}
