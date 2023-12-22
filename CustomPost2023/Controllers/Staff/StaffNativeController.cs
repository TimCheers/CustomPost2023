using CustomPost2023.Data.Calculations;
using CustomPost2023.Data.Models;
using CustomPost2023.Data.Calculations;
using CustomPost2023.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;

using Aspose.Pdf;
using Aspose.Pdf.Text;
using Microsoft.Azure.Amqp.Framing;

namespace CustomPost2023.Controllers.Staff
{
    public class StaffNativeController : Controller
    {
        ApplicationContext db;
        private loggs logg = new loggs();
        public static string meanBefForLogg;
        public StaffNativeController(ApplicationContext context)
        {
            db = context;
        }
        private StaffViewModel DetectStaff(int id)
        {
            var n = db.staff.Where(p => p.id.Equals(id)).FirstOrDefault();
            StaffViewModel model = new StaffViewModel();
            model.staff = n;
            model.custom_post = db.custom_post.Where(p => p.customs_post_id.Equals(n.custom_post_id)).FirstOrDefault();
            model.applications = new List<ApplicationViewModel>();
            List<application> curApp = db.application.Where(p => p.staff_id.Equals(n.id)).ToList();

            foreach (application item in curApp)
            {
                ApplicationViewModel tmp = new ApplicationViewModel();
                tmp.app_app = db.application.FirstOrDefault(p => p.id.Equals(item.id));
                tmp.app_staff = db.staff.FirstOrDefault(p => p.id.Equals(item.staff_id));
                tmp.app_status = db.status.FirstOrDefault(p => p.id.Equals(item.status_id));
                tmp.app_product = db.product.FirstOrDefault(p => p.product_id.Equals(item.product_id));
                tmp.app_custom_post = db.custom_post.FirstOrDefault(p => p.customs_post_id.Equals(item.custom_post_id));
                tmp.app_user = db.user.FirstOrDefault(p => p.user_id.Equals(item.user_id));
                tmp.app_export_country = db.export_countries.FirstOrDefault(p => p.id.Equals(item.staff_id));
                tmp.app_history = db.history.FirstOrDefault(p => p.application_id.Equals(item.id));
                tmp.app_vehicle_type = db.vehicle_type.FirstOrDefault(p => p.vehicle_type_id.Equals(tmp.app_custom_post.fk_vehicle_id));
                tmp.app_prod_type = db.product_type.FirstOrDefault(p => p.type_product_id.Equals(tmp.app_product.fk_type_product_id));
                model.applications.Add(tmp);
            }
            return model;
        }
        private ApplicationViewModel DetectApplication(int id)
        {
            ApplicationViewModel model = new ApplicationViewModel();
            application curApp = db.application.FirstOrDefault(p => p.id.Equals(id));
            model.app_app = curApp;
            model.app_status = db.status.FirstOrDefault(p => p.id.Equals(curApp.status_id));
            model.app_product = db.product.FirstOrDefault(p => p.product_id.Equals(curApp.product_id));
            model.app_custom_post = db.custom_post.FirstOrDefault(p => p.customs_post_id.Equals(curApp.custom_post_id));
            model.app_staff = db.staff.FirstOrDefault(p => p.id.Equals(curApp.staff_id));
            model.app_user = db.user.FirstOrDefault(p => p.user_id.Equals(curApp.user_id));
            model.app_export_country = db.export_countries.FirstOrDefault(p => p.id.Equals(curApp.export_country_id));
            model.app_prod_type = db.product_type.FirstOrDefault(p => p.type_product_id.Equals(model.app_product.fk_type_product_id));
            model.app_history = db.history.FirstOrDefault(p => p.application_id.Equals(curApp.id));
            model.app_vehicle_type = db.vehicle_type.FirstOrDefault(p => p.vehicle_type_id.Equals(model.app_custom_post.fk_vehicle_id));
            return model;
        }
        public IActionResult Index(int id)
        {
            return View(DetectStaff(id));
        }
        [HttpPost]
        public async Task<IActionResult> EditPassword(staff us)
        {
            if (us != null)
            {
                //logg.SendLogg(db, 2, "user", "whole record", $"{us.user_id}|{us.user_name}|{us.login}|{us.password}|{us.role}", "NULL");
                db.staff.Update(us);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult ViewTask(int id)
        {

            return View(DetectApplication(id));
        }
        [HttpPost]
        public IActionResult ViewResult(int id, string oto, string STO, string PGM, string PKPvGM, string IOHT, string conclusion)
        {

            ApplicationViewModel model = DetectApplication(id);

            AppPar temp = new AppPar();
            temp.getHours(oto, STO, PGM, PKPvGM, IOHT, model.app_product.mass, model.app_product.price);

            ResultViewModel resultViewModel = new ResultViewModel();
            resultViewModel.applicationViewModel = model;
            resultViewModel.appPar = temp;
            resultViewModel.conclusion = conclusion;

            return View(resultViewModel);
        }

        public void CreatePDF(ApplicationViewModel app)
        {
            Document pdfDocument = new Document();
            Page page = pdfDocument.Pages.Add();
            
            
            // Оглавление
            TextFragment text = new TextFragment($"Акт досмотра №{app.app_app.id}");
            text.Position = new Position(225, 800);
            text.TextState.FontSize = 16;
            text.TextState.FontStyle = FontStyles.Bold;
            text.TextState.Underline = true;
            page.Paragraphs.Add(text);
            page.Paragraphs.Add(new TextFragment("      "));
            
            TextFragment post = new TextFragment($"{app.app_custom_post.customs_post_title}");
            post.Position = new Position(400, 770);
            post.TextState.FontSize = 12;
            page.Paragraphs.Add(post);
            post = new TextFragment($"{app.app_history.custom_date}");
            post.Position = new Position(400, 730);
            post.TextState.FontSize = 11;
            page.Paragraphs.Add(post);
            
            page.Paragraphs.Add(new TextFragment("      "));
            page.Paragraphs.Add(new TextFragment("      "));
            page.Paragraphs.Add(new TextFragment("      "));
            
            TextSegment text1 = new TextSegment();
            TextSegment text2 = new TextSegment();

            text.TextState.FormattingOptions = new TextFormattingOptions()
            {
                SubsequentLinesIndent = 40
            };
            page.Paragraphs.Add(new TextFragment("      "));
            //id
            text = new TextFragment();
            text2 = new TextSegment("Код товара: ");
            text2.TextState.FontSize = 14;
            text.Segments.Add(text2);
            
            text1 = new TextSegment($"{app.app_product.product_id}");
            text1.TextState.FontSize = 12;
            text1.TextState.Underline = true;
            text.Segments.Add(text1);
            
            page.Paragraphs.Add(text);
            page.Paragraphs.Add(new TextFragment("      "));
            //title
            text = new TextFragment();
            text2 = new TextSegment("Наименование товара: ");
            text2.TextState.FontSize = 14;
            text.Segments.Add(text2);
            
            text1 = new TextSegment($"{app.app_product.product_title}");
            text1.TextState.Underline = true;
            text1.TextState.FontSize = 12;
            text.Segments.Add(text1);
            
            page.Paragraphs.Add(text);
            page.Paragraphs.Add(new TextFragment("      "));
            //typeprod
            text = new TextFragment();
            text2 = new TextSegment("Тип товара: ");
            text2.TextState.FontSize = 14;
            text.Segments.Add(text2);
            
            text1 = new TextSegment($"{app.app_prod_type.type_product_title}");
            text1.TextState.Underline = true;
            text1.TextState.FontSize = 12;
            text.Segments.Add(text1);
            
            page.Paragraphs.Add(text);
            page.Paragraphs.Add(new TextFragment("      "));
            //typevehicle
            text = new TextFragment();
            text2 = new TextSegment("Вид транспорта доставки: ");
            text2.TextState.FontSize = 14;
            text.Segments.Add(text2);
            
            text1 = new TextSegment($"{app.app_vehicle_type.vehicle_type_title}");
            text1.TextState.Underline = true;
            text1.TextState.FontSize = 12;
            text.Segments.Add(text1);
            
            page.Paragraphs.Add(text);
            page.Paragraphs.Add(new TextFragment("      "));
            //user
            text = new TextFragment();
            text2 = new TextSegment("Экспортер: ");
            text2.TextState.FontSize = 14;
            text.Segments.Add(text2);
            
            text1 = new TextSegment($"{app.app_user.user_name}");
            text1.TextState.Underline = true;
            text1.TextState.FontSize = 12;
            text.Segments.Add(text1);
            
            page.Paragraphs.Add(text);
            page.Paragraphs.Add(new TextFragment("      "));
            //country
            text = new TextFragment();
            text2 = new TextSegment("Страна экспорта: ");
            text2.TextState.FontSize = 14;
            text.Segments.Add(text2);
            
            text1 = new TextSegment($"{app.app_export_country.country_title}");
            text1.TextState.Underline = true;
            text1.TextState.FontSize = 12;
            text.Segments.Add(text1);
            
            page.Paragraphs.Add(text);
            page.Paragraphs.Add(new TextFragment("      "));
            //charact
            text = new TextFragment();
            text2 = new TextSegment("Характиристики товара товара: ");
            text2.TextState.FontSize = 14;
            text.Segments.Add(text2);
            
            text1 = new TextSegment($"{app.app_product.characteristics}");
            text1.TextState.Underline = true;
            text1.TextState.FontSize = 12;
            text.Segments.Add(text1);
            
            page.Paragraphs.Add(text);
            page.Paragraphs.Add(new TextFragment("      "));
            //quan
            text = new TextFragment();
            text2 = new TextSegment("Заявленное количество товара: ");
            text2.TextState.FontSize = 14;
            text.Segments.Add(text2);
            
            text1 = new TextSegment($"{app.app_product.quantity}");
            text1.TextState.Underline = true;
            text1.TextState.FontSize = 12;
            text.Segments.Add(text1);
            
            page.Paragraphs.Add(text);
            page.Paragraphs.Add(new TextFragment("      "));
            //mass
            text = new TextFragment();
            text2 = new TextSegment("Заявленная масса товара: ");
            text2.TextState.FontSize = 14;
            text.Segments.Add(text2);
            
            text1 = new TextSegment($"{app.app_product.mass} кг");
            text1.TextState.Underline = true;
            text1.TextState.FontSize = 12;
            text.Segments.Add(text1);
            
            page.Paragraphs.Add(text);
            page.Paragraphs.Add(new TextFragment("      "));
            //price
            text = new TextFragment();
            text2 = new TextSegment("Цена товара: ");
            text2.TextState.FontSize = 14;
            text.Segments.Add(text2);
            
            text1 = new TextSegment($"{app.app_product.price} \u20bd");
            text1.TextState.Underline = true;
            text1.TextState.FontSize = 12;
            text.Segments.Add(text1);
            
            page.Paragraphs.Add(text);
            page.Paragraphs.Add(new TextFragment("      "));
            
            //discr
            text = new TextFragment();
            text2 = new TextSegment("Описание товара: ");
            text2.TextState.FontSize = 14;
            text.Segments.Add(text2);
            
            text1 = new TextSegment($"{app.app_product.description}");
            text1.TextState.Underline = false;
            text1.TextState.FontSize = 12;
            text.Segments.Add(text1);
            
            page.Paragraphs.Add(text);
            page.Paragraphs.Add(new TextFragment("      "));
            
            //my code
            //COST
            text = new TextFragment();
            text2 = new TextSegment("Цена растаможки товара: ");
            text2.TextState.FontSize = 14;
            text.Segments.Add(text2);
            
            text1 = new TextSegment($"{app.app_history.customs_clearance_cost} рублей");
            text1.TextState.Underline = true;
            text1.TextState.FontSize = 12;
            text.Segments.Add(text1);
            
            page.Paragraphs.Add(text);
            
            
            page.Paragraphs.Add(new TextFragment("      "));
            
            //USER NAME
            text = new TextFragment();
            text2 = new TextSegment("Сотрудник: ");
            text2.TextState.FontSize = 14;
            text.Segments.Add(text2);
            
            text1 = new TextSegment($"{app.app_staff.name}");
            text1.TextState.Underline = true;
            text1.TextState.FontSize = 12;
            text.Segments.Add(text1);
            
            page.Paragraphs.Add(text);
            
            
            page.Paragraphs.Add(new TextFragment("      "));
            
            //CONCLUSION
            text = new TextFragment();
            text2 = new TextSegment("Заключение: ");
            text2.TextState.FontSize = 14;
            text.Segments.Add(text2);
            
            text1 = new TextSegment($"{app.app_history.conclusion}");
            text1.TextState.Underline = false;
            text1.TextState.FontSize = 12;
            text.Segments.Add(text1);
            
            page.Paragraphs.Add(text);
            
            
            page.Paragraphs.Add(new TextFragment("      "));
            
            
            

            //Добавление места для подписи
            TextFragment signature = new TextFragment("Дата: ___.___.______         Подпись: ____________________");
            signature.Position = new Position(235, 100); // расположение подписи
            page.Paragraphs.Add(signature);
            

            pdfDocument.Save($"D:\\Акт №{app.app_app.id}.pdf");
        }
        [HttpPost]
        public IActionResult DoneResult(int id, string conclusion, double cusTime, double cusPrise, string radioGroup)
        {

            ApplicationViewModel appModel = DetectApplication(id);
            
            
            history newHi = appModel.app_history;
            newHi.conclusion = conclusion;
            newHi.customs_clearance_time = cusTime;
            newHi.customs_clearance_cost = cusPrise;

            db.history.Update(newHi);
            db.SaveChanges();
            application newApp = appModel.app_app;

            if (int.Parse(radioGroup)==1)
            {           
                newApp.status_id = 5;
            }
            else
            {
                newApp.status_id = 4;
            }

            db.application.Update(newApp);
            db.SaveChanges();

            CreatePDF(appModel);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult ViewAllTasks(int id)
        {
            return View(DetectStaff(id));
        }
    }
}
