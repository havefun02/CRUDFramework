
namespace CRUDFramework.Cores
{
    public class FilterCriterion<T>
    {
        public string? PropertyName { get; set; }
        public T? Value { get; set; }
    }

    public class RangeFilterCriterion<T> : FilterCriterion<T>
    {
        public T? StartValue { get; set; }
        public T? EndValue { get; set; }
    }
}
