using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomPost2023.Data.Models
{
    public class staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int age { get; set; }
        [Required]
        public int work_experience { get; set; }
        [Required]
        public int custom_post_id {  get; set; }
        [Required]
        public string job_title { get; set; }
        [Required] 
        public int phone_number { get; set; }
    }
}
