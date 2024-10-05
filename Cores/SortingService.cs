using CRUDFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CRUDFramework.Cores
{
     class SortingService<T>:ISortingService<T> where T:class
    {
        public IQueryable<T> Sort(IQueryable<T> query, SortingParams sortingParams)
        {
            if (sortingParams == null || sortingParams.sortList == null || !sortingParams.sortList.Any())
            {
                return query;
            }

            if (sortingParams.sortList.Count == 0)
            {
                    query = ApplyDefaultSorting(query);
            }
            IOrderedQueryable<T>? orderedQuery = null;

            for (int i = 0; i < sortingParams.sortList.Count; i++)
            {
                try
                {
                    var sortCriterion = sortingParams.sortList[i];
                    var parameter = Expression.Parameter(typeof(T), "x");
                    var property = Expression.Property(parameter, sortCriterion.PropertyName);

                    var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);

                    if (i == 0)
                    {
                        orderedQuery = sortCriterion.Descending ?
                            query.OrderByDescending(lambda) :
                            query.OrderBy(lambda);
                    }
                    else
                    {
                        orderedQuery = sortCriterion.Descending ?
                            orderedQuery!.ThenByDescending(lambda) :
                            orderedQuery!.ThenBy(lambda);
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

            return orderedQuery ?? query;
        }
        private IQueryable<T> ApplyDefaultSorting(IQueryable<T> query)
        {
            // Get all primary key properties using reflection
            var keyProperties = typeof(T).GetProperties()
                .Where(p => p.GetCustomAttribute<KeyAttribute>() != null)
                .ToList();

            if (keyProperties.Any())
            {
                IOrderedQueryable<T>? orderedQuery = null;
                for (int i = 0; i < keyProperties.Count; i++)
                {
                    var keyProperty = keyProperties[i];
                    var parameter = Expression.Parameter(typeof(T), "x");
                    var property = Expression.Property(parameter, keyProperty.Name);
                    var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);

                    if (i == 0)
                    {
                        orderedQuery = query.OrderBy(lambda);
                    }
                    else
                    {
                        orderedQuery = orderedQuery!.ThenBy(lambda); 
                    }
                }

                return orderedQuery!;
            }

            return query;
        }
    }

}
