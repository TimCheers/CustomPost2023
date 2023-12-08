using CustomPost2023.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CustomPost2023.ViewModels
{
    public class StaffViewModel
    {
        public staff staff {  get; set; }
        public custom_post custom_post { get; set; }
        public List<ApplicationViewModel> applications { get; set; }
    }
}
