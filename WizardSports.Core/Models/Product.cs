using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WizardSports.Core.Models
{
    public class Product : BaseEntity
    {
        [StringLength(50)]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public int StockLevel { get; set; }
        //public decimal DiscountPrice { get; set; } //the discount price for members/premium
        //public decimal Discount { get; set; } //set discount for product
    }
}
