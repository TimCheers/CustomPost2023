using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomPost2023.Data.Models
{
    public class product_type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int type_product_id { get; set; }
        public string type_product_title { get; set; }
        public int customs_clearance_coefficient { get; set; }
    }
}
