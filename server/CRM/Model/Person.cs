using System.ComponentModel.DataAnnotations;

namespace CRM.Model
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
            
        public string LastName { get; set; }
            
        public string Email { get; set; }
            
    }
}
