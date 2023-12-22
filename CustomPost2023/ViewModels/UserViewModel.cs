using CustomPost2023.Data.Models;

namespace CustomPost2023.ViewModels;

public class UserViewModel
{
    public user User {  get; set; }
    public List<ApplicationViewModel> applications { get; set; }
    public ApplicationViewModel new_application { get; set; }
    public List<product_type> ProductTypes { get; set; } 
    public List<vehicle_type> VehicleTypes { get; set; }
    public List<export_countries> ExportCountriesList { get; set; }
}