using System.ComponentModel.DataAnnotations;

namespace CustomPost2023.Data.Models
{
    public class vehicle_type
    {
        [Key]
        public int vehicle_type_id { get; set; }
        public string vehicle_type_title { get; set; }
    }
}
