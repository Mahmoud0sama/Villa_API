using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Villa_API.Data;
using Villa_API.Models;
using Villa_API.Repository.IRepository;

namespace Villa_API.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		internal DbSet<T> dbset;

		public Repository(ApplicationDbContext db)
		{
			_db = db;
			this.dbset = _db.Set<T>();
		}


		public async Task CreateAsync(T entity)
		{
			await dbset.AddAsync(entity);
			await SaveAsync();
		}

		public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
		{
			IQueryable<T> querry = dbset;
			if (!tracked)
			{
				querry = querry.AsNoTracking();
			}
			if (filter != null)
			{
				querry = querry.Where(filter);
			}
			return await querry.FirstOrDefaultAsync();
		}

		public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
		{
			IQueryable<T> querry = dbset;
			if (filter != null)
			{
				querry = querry.Where(filter);
			}
			return await querry.ToListAsync();

		}

		public async Task RemoveAsync(T entity)
		{
			dbset.Remove(entity);
			await SaveAsync();
		}

		public async Task SaveAsync()
		{
			await _db.SaveChangesAsync();
		}

	}
}
