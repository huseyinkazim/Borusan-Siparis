using AutoMapper;
using Borusan.Api.Model;
using Borusan.Business;
using Borusan.Data;
using Borusan.Interface;
using Borusan.Repository;
using Borusan.Repository.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Borusan.Api.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class OrdersController : Controller
	{
		private readonly OrderHandler _orderHandler;
		private readonly MaterialHandler _materialHandler;


		public OrdersController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_orderHandler = new OrderHandler(unitOfWork, mapper);
			_materialHandler = new MaterialHandler(unitOfWork, mapper);
		}

		[HttpPost]
		public List<OrderResult> PostOrder([FromBody] List<OrderDTO> orderDtoList)
		{
			if (orderDtoList == null) return new List<OrderResult> { new OrderResult { Statu = true, HataAciklama = "Order list not exists." } };
			List<MaterialDTO> willAddMaterials = new List<MaterialDTO>();
			List<OrderDTO> willAddOrders = new List<OrderDTO>();
			List<OrderResult> errorOrders = new List<OrderResult>();
			foreach (OrderDTO orderDto in orderDtoList)
			{
				//if (orderDto.Material == null) return BadRequest($"CustomerOrderNo:{orderDto.CustomerOrderNo} of Material not exists.");

				if (!_materialHandler.AnyMaterial(orderDto.MaterialCode))
				{
					//if (string.IsNullOrEmpty(orderDto.Material.MaterialCode))
					//	return BadRequest($"CustomerOrderNo:{orderDto.CustomerOrderNo} of Material's MaterialCode not exists.");
					//else if (string.IsNullOrEmpty(orderDto.Material.MaterialName))
					//	return BadRequest($"CustomerOrderNo:{orderDto.CustomerOrderNo} of Material's MaterialName not exists.");
					//else 
					if (!willAddMaterials.Any(i => i.MaterialCode != orderDto.MaterialCode))
						willAddMaterials.Add(new MaterialDTO { MaterialCode = orderDto.MaterialCode, MaterialName = orderDto.MaterialName });
				}
				if (_orderHandler.AnyOrderWithCustomerCode(orderDto))
					errorOrders.Add(new OrderResult { MusteriSiparisNo = orderDto.CustomerOrderNo, Statu = false, HataAciklama = "Order already exists." });
				else if (!willAddOrders.Any(i => i.CustomerOrderNo == orderDto.CustomerOrderNo && i.CustomerCode == orderDto.CustomerCode))
					willAddOrders.Add(orderDto);

			}
			_materialHandler.AddMaterials(willAddMaterials);
			_orderHandler.AddOrders(willAddOrders);
			_orderHandler.Commit();

			var resultList = willAddOrders.Select(i => new OrderResult { Statu = true, MusteriSiparisNo = i.CustomerOrderNo }).ToList();
			resultList.AddRange(errorOrders);

			return resultList;
		}

		[HttpPost]
		public ServiceResponse<OrderStatusModel> SetOrderState([FromBody] OrderStatusModel orderDto)
		{
			
			if (string.IsNullOrEmpty(orderDto.CustomerOrderNo) || orderDto.ChangeDate == default(DateTime) || orderDto.Status == null)
				return new ServiceResponse<OrderStatusModel>("Order model state not true.");

			if (!_orderHandler.AnyOrder(orderDto))
				return new ServiceResponse<OrderStatusModel>("Order not found");

			if (_orderHandler.SetOrderState(orderDto) && _orderHandler.Commit() == 1)
				return new ServiceResponse<OrderStatusModel>(orderDto);
			return new ServiceResponse<OrderStatusModel>("Order not changed");

		}

		[HttpGet]
		public ServiceResponse<OrderStatusModel> GetOrderState([FromBody] OrderStatusModel orderDto)
		{
			if (!_orderHandler.AnyOrder(orderDto))
				return new ServiceResponse<OrderStatusModel>("Order not found");

			return new ServiceResponse<OrderStatusModel>(_orderHandler.GetOrderState(orderDto));

		}
		[HttpGet]
		public List<OrderDTO> GetAllOrders()
		{
			return _orderHandler.GetAllOrders();
		}
	}
}
