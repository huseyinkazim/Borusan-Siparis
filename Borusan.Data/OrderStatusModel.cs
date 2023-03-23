using Borusan.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borusan.Data
{
	public class OrderStatusModel
	{
		public int OrderNo { get; set; }
		public string? CustomerOrderNo { get; set; }
		public OrderStatus? Status { get; set; }
		public string? CustomerCode { get; set; }

		public DateTime ChangeDate { get; set; }
	}
}
