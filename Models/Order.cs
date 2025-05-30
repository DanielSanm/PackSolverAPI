namespace PackSolverAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<ProductBox> ProductBoxes { get; set; }
    }
}
