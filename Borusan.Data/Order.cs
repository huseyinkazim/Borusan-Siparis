using Borusan.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace Borusan.Data
{
	public class Order
	{
		[Key]
		public int OrderNo { get; set; }
		public string? CustomerOrderNo { get; set; }
		public string? SourceAddress { get; set; }
		public string? DestinationAddress { get; set; }
		public decimal Quantity { get; set; }
		public QuantityUnit QuantityUnit { get; set; }
		public decimal Weight { get; set; }
		public WeightUnit WeightUnit { get; set; }
		public string? MaterialCode { get; set; }
		public string? MaterialName { get; set; }
		//public Material? Material { get; set; }
		public string? Note { get; set; }
		public OrderStatus OrderStatus { get; set; } = OrderStatus.SiparisAlindi;
		public string? CustomerCode { get; set; }
		public DateTime ChangeDate { get; set; } = DateTime.Now;
	}
}