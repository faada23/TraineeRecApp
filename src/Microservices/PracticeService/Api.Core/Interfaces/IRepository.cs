using System.Linq.Expressions;
using Api.Core.AdditionalClasses;
using Api.Core.Entities;
using Api.Core.Interfaces;

public interface IRepository<T> where T : class, IEntity
{
    Task<Result<int>> Insert(T entity);
    Task<Result<int>> Delete(int id);
    Task<Result<int>> Update(T entity);
    Task<Result<T>> GetById(int id, string? includeProperties = null);
    Task<Result<IEnumerable<T>>> GetAll(IFilter<T>? filter, PaginationParameters? parameters = null, string? includeProperties = null);
}