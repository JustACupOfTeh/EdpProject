using System.ComponentModel.DataAnnotations;

namespace eCO2Tracker.Models
{
	public class Activity
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public decimal Units { get; set; }
        public virtual int PhoneTypeId
        {
            get
            {
                return (int)this.Category;
            }
            set
            {
                Category = (Category)value;
            }
        }
        [EnumDataType(typeof(Category))]
        public Category Category { get; set; }
		public bool IsPerformed { get; set; }
	}
}
