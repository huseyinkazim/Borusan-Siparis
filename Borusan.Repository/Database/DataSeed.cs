using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borusan.Repository.Database
{
	public class DataSeed
	{
		private static ApplicationDbContext _context;

		public static void Initialize(IServiceProvider serviceProvider)
		{


			if (CheckSeed(serviceProvider))
				return;
			var material = new Data.Material { MaterialCode = "MaterialCode", MaterialName = "MaterialName" };
			_context.Materials.Add(material);
			_context.Orders.Add(new Data.Order
			{
				CustomerOrderNo = "1",
				CustomerCode = "1",
				SourceAddress = "SourceAddress",
				DestinationAddress = "DestinationAddress",
				Note = "Note",
				MaterialCode = "MaterialCode",
				MaterialName = "MaterialName",
				OrderStatus = Data.Enum.OrderStatus.SiparisAlindi,
				Quantity = 1,
				QuantityUnit = Data.Enum.QuantityUnit.Adet,
				Weight = 10,
				WeightUnit = Data.Enum.WeightUnit.Kg,
				ChangeDate = DateTime.Now,
			});
			_context.SaveChanges();
		}
		private static bool CheckSeed(IServiceProvider serviceProvider)
		{
			_context = serviceProvider.GetRequiredService<ApplicationDbContext>();

			_context.Database.EnsureCreated();

			return _context.Orders.Any();

		}
	}
}
