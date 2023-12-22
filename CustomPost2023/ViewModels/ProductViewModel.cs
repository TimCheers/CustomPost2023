using CustomPost2023.Data.Models;

namespace CustomPost2023.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<product> productList { get; set; }
        public IEnumerable<export_countries> export_countriesList { get; set; }
        public IEnumerable<user> userList { get; set; }
        public IEnumerable <product_type> productTypeList { get; set; }
        public IEnumerable<vehicle_type> vehicleTypeList { get; set; }
        public IEnumerable<status> statusList { get; set; }
        public IEnumerable<custom_post> custom_postList { get; set; }
        public IEnumerable<application> applicationList { get; set; }

    }
}
