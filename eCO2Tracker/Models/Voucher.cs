using System.ComponentModel.DataAnnotations;

namespace eCO2Tracker.Models
{
    public class Voucher
    {
        public string VoucherID { get; set; } = string.Empty;
        public string VoucherName { get; set; }
        public string VoucherUrl { get; set; }
        public int VoucherPrice { get; set; }
        public float VoucherDiscount { get; set; }
        public string VoucherType { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ExpiredDate { get; set; }

    }
}
