namespace PackSolverAPI.Models
{
    public class ProductBox
    {
        public string ProductId { get; set; }
        public Product Product { get; set; }

        public string BoxId { get; set; }
        public Box Box { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }


    }
}
