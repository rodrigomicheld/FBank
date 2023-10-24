using FBank.Domain.Enums;
using System.Linq.Expressions;

namespace FBank.Infrastructure.Common
{
    public static class QueryableExtensions
    {
        public static IQueryable<TSource> Sort<TSource>(this IQueryable<TSource> source, string[] sort)
        {
            if (sort is null || sort.Length == 0)
                return source;

            foreach (var sortParameters in sort)
            {
                string[] parameters = sortParameters.Split(" ");
                if (parameters is null || parameters.Length == 0)
                    continue;

                string columnName = parameters[0];

                if (string.IsNullOrEmpty(columnName))
                    continue;

                string order = parameters.Length > 1 ? parameters[1].ToUpper() : "ASC";

                var orderExpression = GenerateExpression<TSource>(columnName);

                source = order == OrderBySort.ASC.ToString() ? source.OrderBy(orderExpression) : source.OrderByDescending(orderExpression);
            }
            return source;
        }

        private static Expression<Func<TSource, Object>> GenerateExpression<TSource>(string propertyPath)
        {
            var param = Expression.Parameter(typeof(TSource), "x");
            Expression body = param;

            foreach (var member in propertyPath.Split('.'))
            {
                body = Expression.PropertyOrField(body, member);
            }
            var orderExpression = Expression.Lambda<Func<TSource, Object>>(Expression.Convert(body, typeof(object)), param);
            
            return orderExpression;
        }
    }
}
