using PackSolverAPI.Models;

namespace PackSolverAPI.DTO
{
    public class OrderPackingRequest
    {
        public int OrderId { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
