using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models;

namespace WebShop.Model.ViewModel
{
	public class ShoppingCartVM
	{
		public IEnumerable<ShoppingCart> ListCart { get; set; }
		public double CartTotal { get; set; }

		public OrderHeader OrderHeader { get; set; }
	}
}
