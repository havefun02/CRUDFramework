using CRUDFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRUDFramework.Cores
{
    internal class FilterService<T> : IFilterService<T> where T : class
    {
        public IQueryable<T> Filter(IQueryable<T> query, FilterParams<object> filterParams)
        {
            if (filterParams == null || filterParams.filterList == null) return query;

            foreach (var filter in filterParams.filterList)
            {
                try
                {
                    var parameter = Expression.Parameter(typeof(T), "x");

                    if (string.IsNullOrEmpty(filter.PropertyName))
                    {
                        throw new ArgumentNullException(nameof(filter.PropertyName), "Property name cannot be null or empty.");
                    }

                    var property = Expression.Property(parameter, filter.PropertyName);

                    if (filter is RangeFilterCriterion<object> rangeFilter)
                    {
                        Expression? body = null;

                        if (rangeFilter.StartValue != null)
                        {
                            var startValue = Expression.Constant(rangeFilter.StartValue);
                            var greaterThanOrEqual = Expression.GreaterThanOrEqual(property, startValue);
                            body = greaterThanOrEqual;
                        }

                        if (rangeFilter.EndValue != null)
                        {
                            var endValue = Expression.Constant(rangeFilter.EndValue);
                            var lessThanOrEqual = Expression.LessThanOrEqual(property, endValue);
                            body = body != null ? Expression.AndAlso(body, lessThanOrEqual) : lessThanOrEqual;
                        }

                        if (body != null)
                        {
                            var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
                            query = query.Where(lambda);
                        }
                    }
                    else
                    {
                        if (filter.Value == null)
                        {
                            throw new ArgumentNullException(nameof(filter.Value), $"{filter.PropertyName} value cannot be null.");
                        }

                        var value = Expression.Constant(filter.Value);
                        var body = Expression.Equal(property, value);
                        var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
                        query = query.Where(lambda);
                    }
                }
                catch (ArgumentNullException ex)
                {
                    throw new InvalidDataException("Property name cannot be null.", ex);
                }
                catch (ArgumentException ex)
                {
                    throw new InvalidDataException("Property name is incorrect.", ex);
                }
            }

            return query;
        }
    }
}
