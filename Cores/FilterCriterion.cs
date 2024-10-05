
namespace CRUDFramework.Cores
{
     class FilterCriterion<T>
    {
        public string? PropertyName { get; set; }
        public T? Value { get; set; }
    }

     class RangeFilterCriterion<T> : FilterCriterion<T>
    {
        public T? StartValue { get; set; }
        public T? EndValue { get; set; }
    }
}
