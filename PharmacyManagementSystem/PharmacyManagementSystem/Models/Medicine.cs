
namespace PharmacyManagementSystem.Models
{
    public class Medicine
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";
        public string Barcode { get; set; } = "";
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ExpiryDate { get; set; } = "";
    }
}
