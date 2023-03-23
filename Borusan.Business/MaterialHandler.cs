using AutoMapper;
using Borusan.Data;
using Borusan.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borusan.Business
{
	public class MaterialHandler
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public MaterialHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public bool AnyMaterial(string MaterialCode)
		{
			return _unitOfWork.Materials.AnyMaterial(MaterialCode);
		}

		public void AddMaterials(List<MaterialDTO> dtoObjectList)
		{
			List<Material> entityList = _mapper.Map<List<Material>>(dtoObjectList);
			_unitOfWork.Materials.AddMaterials(entityList);
		}
	}
}
