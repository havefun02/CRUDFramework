namespace CRUDFramework
{
     public interface ISortingService<T> where T : class
    {
       IQueryable<T> Sort(IQueryable<T> query, SortingParams orderParams);

    }
}
