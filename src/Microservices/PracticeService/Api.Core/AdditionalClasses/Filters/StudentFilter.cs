using Api.Core.Entities;
using System.Linq.Expressions;

namespace Api.Core.AdditionalClasses.Filters;

public class StudentFilter : IFilter<Student>
{
    public string? GroupName { get; set; }
    public string? Specialty { get; set; }

    
    public Expression<Func<Student, bool>> ToExpression()
    {
        throw new NotImplementedException();
    }
}