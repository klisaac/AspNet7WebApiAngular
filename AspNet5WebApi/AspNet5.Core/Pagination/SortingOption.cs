namespace AspNet5.Core.Pagination
{
    public class SortingOption
    {
        public string Field { get; set; }

        public SortingDirection Direction { get; set; }

        public int Priority { get; set; }

        public enum SortingDirection
        {
            ASC,
            DESC
        }
    }
}
