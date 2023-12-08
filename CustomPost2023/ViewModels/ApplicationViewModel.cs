using CustomPost2023.Data.Models;

namespace CustomPost2023.ViewModels
{
    public class ApplicationViewModel
    {
        public application app_app {  get; set; }
        public product app_product {  get; set; }
        public custom_post app_custom_post {  get; set; }
        public staff app_staff { get; set; }
        public status app_status { get; set; }
        public user app_user { get; set; }
        public export_countries app_export_country {  get; set; }
        public history app_history { get; set; }
        public product_type app_prod_type { get; set; }
    }
}
