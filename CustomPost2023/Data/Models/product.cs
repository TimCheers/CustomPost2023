using System.ComponentModel.DataAnnotations;

namespace CustomPost2023.Data.Models
{
    public class products
    {
        [Key]
        public int product_id { get; set; }
        [Required]
        [Display(Name = "Название продукта")]
        public string product_title { get; set; }
        public string export_country { get; set; }
        public int mass { get; set; }
        public int fk_user_id { get; set; }
        public int fk_type_product_id { get; set; }
        public int fk_vehicle_id { get; set; }
    }
}
