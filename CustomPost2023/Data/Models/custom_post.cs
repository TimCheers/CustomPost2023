using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomPost2023.Data.Models
{
    public class custom_post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int customs_post_id { get; set; }
        public string customs_post_title { get; set; }
        public string location { get; set; }
        public int throughput { get; set; }
        public int fk_vehicle_id { get; set; }
    }
}
