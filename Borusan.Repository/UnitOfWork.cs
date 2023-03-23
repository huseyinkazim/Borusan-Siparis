using Borusan.Interface;
using Borusan.Repository.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borusan.Repository
{
	public class UnitOfWork: IUnitOfWork
	{
		private readonly ApplicationDbContext _context;
		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
			Orders = new OrderRepository(_context);
			Materials = new MaterialRepository(_context);
		}
		public IOrderRepository Orders { get; private set; }

		public IMaterialRepository Materials { get; private set; }

		public int Commit() => _context.SaveChanges();
		public async Task<int> CommitAsync() => await _context.SaveChangesAsync();
		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
