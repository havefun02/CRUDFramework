namespace CRUDFramework
{
    public class OffsetPaginationResult<T> : IPaginationResult<T> where T : class
    {
        public int offset { get; set; } 
        public int totalItems { get ; set; }
        public List<T>? items { get ; set ; }
        public int limit { get ; set ; }
    }
}
