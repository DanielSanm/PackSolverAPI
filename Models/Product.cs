namespace PackSolverAPI.Models
{
    public class Product
    {
        public string ProductId { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<ProductBox>? productBoxes { get; set; }
    }
}
