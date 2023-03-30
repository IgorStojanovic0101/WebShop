using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models;

namespace WebShop.Model.ViewModel
{
	public class OrderVM
	{
		public OrderHeaderModel OrderHeader { get; set; }
		public IEnumerable<OrderDetailModel> OrderDetail { get; set; }
	}
}
