namespace CRUDFramework
{
     public interface IPaginationResult<T> where T : class
    {
        public int totalItems { get; set; }
        public List<T>? items { get; set; }
        public int limit { get; set; }
    }
}
