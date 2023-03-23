using Borusan.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borusan.Interface
{
	public interface IMaterialRepository
	{
		bool AnyMaterial(string MaterialCode);
		void AddMaterials(List<Material> materials);
	}
}
