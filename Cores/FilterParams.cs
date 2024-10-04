namespace CRUDFramework.Cores
{
    internal class FilterParams<T>
    {
        public List<FilterCriterion<T>>? filterList { set; get; }

    }
}
