using AutoMapper;
using Borusan.Data;

namespace Borusan.Api.Extensions
{
	public class MappingProfile:Profile
	{
		public MappingProfile()
		{
			CreateMap<Order, OrderDTO>().ReverseMap();
			CreateMap<Material, MaterialDTO>().ReverseMap();
		}
	}
}
