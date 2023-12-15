using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomPost2023.Data.Models
{
    public class product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int product_id { get; set; }
        [Required]
        [Display(Name = "Название продукта")]
        public string product_title { get; set; }
        public int mass { get; set; }
        public int fk_type_product_id { get; set; }
        public string description { get; set; }
        public string characteristics { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }

    }
}
