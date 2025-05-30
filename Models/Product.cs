namespace PackSolverAPI.Models
{
    public class Product
    {
        public string ProductId { get; set; }
        public string Dimensions { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
