using Borusan.Data;
using Borusan.Interface;
using Borusan.Repository.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borusan.Repository
{
	public class MaterialRepository : IMaterialRepository
	{
		private readonly ApplicationDbContext _dbContext;

		public MaterialRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public bool AnyMaterial(string MaterialCode)
		{
			return _dbContext.Materials.Any(i => i.MaterialCode == MaterialCode);
		}
		public void AddMaterials(List<Material> materials)
		{
			_dbContext.Materials.AddRange(materials);
		}
	}
}
