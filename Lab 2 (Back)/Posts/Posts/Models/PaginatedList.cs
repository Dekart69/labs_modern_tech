namespace Posts.Models
{
    public class PaginatedList<T>
    {
        public PaginatedList(List<T> items, int limit, int offset, int totalCount)
        {
            Items = items;
            Limit = limit;
            Offset = offset;
            TotalCount = totalCount;
        }

        public List<T> Items { get; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int TotalCount { get; set; }
    }
}
