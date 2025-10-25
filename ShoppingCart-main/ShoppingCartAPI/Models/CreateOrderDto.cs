using System.ComponentModel.DataAnnotations;

namespace ShoppingCartAPI.Models
{
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "Item code is required")]
        [StringLength(50)]
        public string ItemCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Item name is required")]
        [StringLength(200)]
        public string ItemName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int ItemQty { get; set; }

        [Required(ErrorMessage = "Delivery date is required")]
        public DateTime OrderDelivery { get; set; }

        [Required(ErrorMessage = "Delivery address is required")]
        [StringLength(500)]
        public string OrderAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}

