using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomPost2023.Data.Models
{
    public class application
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public int product_id { get; set; }
        [Required]
        public int custom_post_id { get; set; }
        [Required]
        public int staff_id { get; set; }
        [Required]
        public int status_id { get; set; }
        [Required]
        public int user_id { get; set; }
        [Required]
        public int export_country_id { get; set; }
    }
}
