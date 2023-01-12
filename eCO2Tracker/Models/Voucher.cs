using System.ComponentModel.DataAnnotations;

namespace eCO2Tracker.Models
{
    public class Voucher
    {
        [Key]
        public string VoucherID { get; set; } = string.Empty;
        public string VoucherName { get; set; } = string.Empty;
        public string VoucherUrl { get; set; } = string.Empty;
        public int VoucherPrice { get; set; }
        public float VoucherDiscount { get; set; }
        public string VoucherType { get; set; } = string.Empty;
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ExpiredDate { get; set; }

    }
}
