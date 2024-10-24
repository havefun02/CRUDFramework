namespace CRUDFramework
{
    public class OffsetPaginationParams : IPaginationParams
    {
        public int limit { get; set; } = 12;
        public int offset { get; set; } = 0;
    }
}
