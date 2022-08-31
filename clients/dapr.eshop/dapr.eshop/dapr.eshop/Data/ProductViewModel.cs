namespace dapr.eshop.Data
{
    public class ProductViewModel
    {
        public ProductViewModel()
        { }

        public Guid Id { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Rating { get; set; }
    }
}
