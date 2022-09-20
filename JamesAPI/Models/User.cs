using System.ComponentModel.DataAnnotations;

namespace JamesAPI.Models
{
    public class User :IguidInterface
    {
        [Key]
        public Guid Uid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }   
        public string MobilePhone { get; set; }
        public string Password { get; set; }
        public string AuthCode { get; set; }

        
    }
}
