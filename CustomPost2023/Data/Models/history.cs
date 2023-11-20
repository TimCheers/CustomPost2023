using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomPost2023.Data.Models
{
    public class history
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int application_id { get; set; }
        public DateOnly custom_date { get; set; }
    }
}
