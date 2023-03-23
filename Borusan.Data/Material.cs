using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borusan.Data
{
	public class Material
	{
		[Key]
		public string MaterialCode { get; set; }
		public string? MaterialName { get; set; }
	}
}
