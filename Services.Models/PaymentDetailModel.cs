using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Services.Models
{
    public class PaymentDetailModel
    {
        [Key]
        public int PaymentDetailId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string CardOwnerName { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(16)")]
        public string CardNumber { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(5)")]
        public string ExpirationDate { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(3)")]
        public string SecurityCode { get; set; } = string.Empty;
    }
}