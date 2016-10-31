using System.ComponentModel.DataAnnotations;

namespace Assignment3.Services.Entities
{
    public class Token
    {
        [Key]
        public int userID { get; set; }
        public string token { get; set; }
        public string expires { get; set; }
    }
}