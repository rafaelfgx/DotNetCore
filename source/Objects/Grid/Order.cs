namespace DotNetCore.Objects
{
    public class Order
    {
        public Order()
        {
            Ascending = true;
        }

        public bool Ascending { get; set; }

        public string Property { get; set; }
    }
}
