namespace DotNetCore.Objects
{
    public sealed class Filter
    {
        public string Property { get; set; }

        public string Comparison { get; set; }

        public string Value { get; set; }
    }
}
