using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CustomPost2023.Data.Models
{
    public class loggs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public DateTime datetime { get; set; }
        public int user_id { get; set; }
        public string action { get; set; }
        public string table { get; set; }
        public string attribute { get; set; }
        public string meaning_before { get; set; }
        public string meaning_now { get; set; }
        public void SendLogg(ApplicationContext context, int actionType, string table, string attribute, string meaning_before, string meaning_now)
        {
            var actionDic = new Dictionary<int, string>()
            {
                [1] = "Adding an entry",
                [2] = "Deleting an entry",
                [3] = "Editing an entry"
            };
            this.action = actionDic[actionType];
            datetime = DateTime.Now.ToUniversalTime();
            this.user_id = 9999;
            this.table = table;
            this.attribute = attribute;
            this.meaning_before = meaning_before;
            this.meaning_now = meaning_now;
            context.loggs.Add(this);
        }
    }
}
