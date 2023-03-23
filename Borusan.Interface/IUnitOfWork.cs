using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borusan.Interface
{
	public interface IUnitOfWork
	{
		IOrderRepository Orders { get; }
		IMaterialRepository Materials { get; }
		int Commit();
		Task<int> CommitAsync();

	}
}
