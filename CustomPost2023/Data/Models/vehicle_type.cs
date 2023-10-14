using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomPost2023.Data.Models
{
    public class vehicle_type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int vehicle_type_id { get; set; }
        public string vehicle_type_title { get; set; }
    }
}
