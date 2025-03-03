using Api.Core.AdditionalClasses;
using Api.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

public class Repository<T> : IRepository<T> where T : class, IEntity
{
    private DbContext _db { get; }
    private DbSet<T> dbSet;

    public Repository(DbContext db)
    {
        _db = db;
        dbSet = _db.Set<T>();
    }

    public async Task<Result<int>> Insert(T entity)
    {
        try
        {
            dbSet.Add(entity);
            await _db.SaveChangesAsync();

            return Result<int>.Success(entity.Id, $"Successful Insert");
        }
        catch (Exception ex)
        {
            return Result<int>.Failure($"Error: {ex.Message}");
        }
    }

    public async Task<Result<int>> Delete(int id)
    {
        try
        {
            T? entity = await dbSet.FindAsync(id);
            if (entity == null)
            {
                return Result<int>.Failure("Object not found");
            }

            dbSet.Remove(entity);
            await _db.SaveChangesAsync();

            return Result<int>.Success(entity.Id, "Object successfuly deleted");
        }
        catch (Exception ex)
        {
            return Result<int>.Failure($"Error : {ex.Message}");
        }
    }

    public async Task<Result<IEnumerable<T>>> GetAll(IFilter<T>? filter = null, PaginationParameters? parameters = null, string? includeProperties = null)
    {
        try
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter.ToExpression());
            }

            var totalItems = await query.CountAsync();

            if (parameters != null)
            {
                query = query
                    .Skip((parameters.Page - 1) * parameters.PageSize)
                    .Take(parameters.PageSize);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                query = IncludeProperties(query, includeProperties);
            }

            var list = await query.ToListAsync();
            return Result<IEnumerable<T>>.Success(list, "Records received.");
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<T>>.Failure($"Error : {ex.Message}");
        }
    }

    public async Task<Result<T>> GetById(int id, string? includeProperties = null)
    {
        try
        {
            IQueryable<T> query = dbSet;

            query = query.Where(u => u.Id == id);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                query = IncludeProperties(query, includeProperties);
            }

            var entity = await query.FirstOrDefaultAsync();
            if (entity == null)
            {
                return Result<T>.Failure("Object not found");
            }

            return Result<T>.Success(entity, "Record received");
        }
        catch (Exception ex)
        {
            return Result<T>.Failure($"Error: {ex.Message}");
        }
    }

    public async Task<Result<int>> Update(T entity)
    {
        try
        {
            dbSet.Update(entity);
            await _db.SaveChangesAsync();
            
            return Result<int>.Success(entity.Id, "Successful update");
        }
        catch (Exception ex)
        {
            return Result<int>.Failure($"Error : {ex.Message}");
        }
    }

    private IQueryable<T> IncludeProperties(IQueryable<T> query, string includeProperties)
    {
        foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProp);
        }

        return query;
    }
}
