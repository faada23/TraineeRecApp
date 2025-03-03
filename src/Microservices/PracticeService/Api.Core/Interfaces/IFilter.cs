using System.Linq.Expressions;

public interface IFilter<T> 
{
    public Expression<Func<T, bool>> ToExpression();
}