using Borusan.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace Borusan.Data
{

	public class OrderDTO
	{
		public int OrderNo { get; set; }
		[Required]
		public string CustomerOrderNo { get; set; }
		[Required]
		public string SourceAddress { get; set; }
		[Required]
		public string DestinationAddress { get; set; }
		[Required]
		public decimal Quantity { get; set; }
		[Required]
		public QuantityUnitDTO QuantityUnit { get; set; }
		[Required]
		public decimal Weight { get; set; }
		[Required]
		public WeightUnitDTO WeightUnit { get; set; }
		[Required]
		public string MaterialCode { get; set; }
		[Required]
		public string MaterialName { get; set; }
		[Required]
		public string Note { get; set; }
		public OrderStatusDTO OrderStatus { get; set; } = OrderStatusDTO.SiparisAlindi;
		[Required]
		public string CustomerCode { get; set; }
		public DateTime ChangeDate { get; set; }

	}
}