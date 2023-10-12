using System.ComponentModel.DataAnnotations;

namespace CustomPost2023.Data.Models
{
    public class product_type
    {
        [Key]
        public int type_product_id { get; set; }
        public string type_product_title { get; set; }
        public int customs_clearance_coefficient { get; set; }
    }
}
