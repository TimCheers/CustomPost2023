using CustomPost2023.Data.Models;

namespace CustomPost2023.ViewModels;

public class UserViewModel
{
    public user User {  get; set; }
    public List<ApplicationViewModel> applications { get; set; }
    public ApplicationViewModel new_application { get; set; }
}