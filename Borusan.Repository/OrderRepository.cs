using Borusan.Data;
using Borusan.Interface;
using Borusan.Repository.Database;
using Microsoft.EntityFrameworkCore;

namespace Borusan.Repository
{
	public class OrderRepository : IOrderRepository
	{
		private readonly ApplicationDbContext _dbContext;

		public OrderRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public bool AnyOrder(Order order)
		{
			return _dbContext.Orders.Any(i => i.CustomerOrderNo == order.CustomerOrderNo);
		}
		public bool AnyOrderWithCustomerCode(Order order)
		{
			return _dbContext.Orders.Any(i => i.CustomerOrderNo == order.CustomerOrderNo && i.CustomerCode == order.CustomerCode);
		}
		public Order? GetOrderWithMaterial(Order order)
		{
			return _dbContext.Orders.FirstOrDefault(i => i.CustomerOrderNo == order.CustomerOrderNo && i.CustomerCode == order.CustomerCode);
		}
		public Order? GetOrderWithMaterialByCustomerOrderNo(Order order)
		{
			return _dbContext.Orders.FirstOrDefault(i => i.CustomerOrderNo == order.CustomerOrderNo) ;
		}
		public void AddOrders(List<Order> orderList)
		{
			_dbContext.Orders.AddRange(orderList);
		}
		public void UpdateOrder(Order order)
		{
			_dbContext.Orders.Update(order);
		}
	}
}