using System.ComponentModel.DataAnnotations.Schema;

namespace log_and_reg.Models
{
    [Table("users")]
    public class User
    {
        public int id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
    }
}
