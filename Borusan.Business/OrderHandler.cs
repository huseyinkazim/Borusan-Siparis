using AutoMapper;
using Borusan.Data;
using Borusan.Data.Enum;
using Borusan.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borusan.Business
{
	public class OrderHandler
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public OrderHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public void AddOrders(List<OrderDTO> dtoObjects)
		{
			List<Order> entity = _mapper.Map<List<Order>>(dtoObjects);
			entity.ForEach(i => i.ChangeDate = DateTime.Now);
			_unitOfWork.Orders.AddOrders(entity);
		}

		public OrderDTO GetOrderWithMaterial(OrderDTO dtoObject)
		{
			Order entity = _mapper.Map<Order>(dtoObject);

			_unitOfWork.Orders.GetOrderWithMaterial(entity);
			OrderDTO resDto = _mapper.Map<OrderDTO>(dtoObject);

			return resDto;
		}
		public Order? GetOrderWithMaterialByCustomerOrderNo(Order entity)
		{
			return _unitOfWork.Orders.GetOrderWithMaterialByCustomerOrderNo(entity);

		}

		public bool AnyOrder(OrderStatusModel dtoObject)
		{
			Order entity = new Order { CustomerOrderNo = dtoObject.CustomerOrderNo };

			return _unitOfWork.Orders.AnyOrder(entity);
		}
		public bool AnyOrderWithCustomerCode(OrderDTO dtoObject)
		{
			Order entity = _mapper.Map<Order>(dtoObject);

			return _unitOfWork.Orders.AnyOrderWithCustomerCode(entity);
		}
		public bool SetOrderState(OrderStatusModel dtoObject)
		{
			Order entity = new Order { CustomerOrderNo = dtoObject.CustomerOrderNo };
			var res = GetOrderWithMaterialByCustomerOrderNo(entity);
			if (res.OrderStatus == dtoObject.Status)
				return false;
			res.OrderStatus = (OrderStatus)dtoObject.Status;
			res.ChangeDate = dtoObject.ChangeDate;

			_unitOfWork.Orders.UpdateOrder(res);
			return true;
		}
		public OrderStatusModel? GetOrderState(OrderStatusModel dtoObject)
		{
			Order entity = new Order { CustomerOrderNo = dtoObject.CustomerOrderNo };
			var res = GetOrderWithMaterialByCustomerOrderNo(entity);
			if (res != null)
				return new OrderStatusModel { OrderNo = res.OrderNo, CustomerOrderNo = res.CustomerOrderNo, CustomerCode = res.CustomerCode, Status = res.OrderStatus, ChangeDate = res.ChangeDate };
			return null;

		}
		public List<OrderDTO> GetAllOrders()
		{
			var list = _unitOfWork.Orders.GetAllOrders();
			if (list == null || list.Count == 0)
				return null;
			var listDTO = _mapper.Map<List<OrderDTO>>(list);

			return listDTO;

		}

		public int Commit()
		{
			return _unitOfWork.Commit();
		}
	}
}
