namespace Shop.Domain
{
    public class BaseFilter
    {
        public string Search { get; set; }

        public int Count { get; set; }

        public int SkipCount { get; set; }
    }
}
