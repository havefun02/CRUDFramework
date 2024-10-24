namespace CRUDFramework
{
     public interface IFilterService<T> where T : class
    {
        IQueryable<T> Filter(IQueryable<T> query, FilterParams<object> filterParams);
    }
}
