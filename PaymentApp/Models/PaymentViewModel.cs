using System.ComponentModel.DataAnnotations;

namespace PaymentApp.Models
{
    public class PaymentViewModel
    {
        public int PaymentDetailId { get; set; }

        [Required(ErrorMessage = "Card Owner Name is required.")]
        [StringLength(100, ErrorMessage = "Card Owner Name should not exceed 100 characters.")]
        public string CardOwnerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Card Number is required.")]
        [CreditCard(ErrorMessage = "Invalid credit card number.")]
        public string CardNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Expiration Date is required.")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/?([0-9]{2})$", ErrorMessage = "Invalid Expiration Date. Use MM/YY format.")]
        public string ExpirationDate { get; set; } = string.Empty;

        [Required(ErrorMessage = "Security Code is required.")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Security Code should be exactly 3 characters.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Security Code should contain only numbers.")]
        public string SecurityCode { get; set; } = string.Empty;
    }
}
