using Borusan.Data;

namespace Borusan.Interface
{
	public interface IOrderRepository
	{
		void AddOrders(List<Order> orderList);
		void UpdateOrder(Order order);
		Order? GetOrderWithMaterial(Order order);
		Order? GetOrderWithMaterialByCustomerOrderNo(Order order);
		bool AnyOrder(Order order);
		bool AnyOrderWithCustomerCode(Order order);
		List<Order> GetAllOrders();
	}
}