﻿namespace CRUDFramework
{
     public interface IPaginationService<T> where T : class
    {
         Task<IPaginationResult<T>> Paginate(IQueryable<T> query, IPaginationParams pageParams);
            IPaginationResult<T> Paginate(IEnumerable<T> query, IPaginationParams pageParams);

    }
}
