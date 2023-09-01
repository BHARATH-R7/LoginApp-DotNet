using System.ComponentModel.DataAnnotations;

namespace LoginServiceWithJWT.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public Boolean IsDeleting { get; set; }


    }
    
}
