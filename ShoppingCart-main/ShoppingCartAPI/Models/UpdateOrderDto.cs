using System.ComponentModel.DataAnnotations;

namespace ShoppingCartAPI.Models
{
    public class UpdateOrderDto
    {
        [Required(ErrorMessage = "Delivery date is required")]
        public DateTime OrderDelivery { get; set; }

        [Required(ErrorMessage = "Delivery address is required")]
        [StringLength(500)]
        public string OrderAddress { get; set; } = string.Empty;
    }
}

