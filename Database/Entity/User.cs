using System.ComponentModel.DataAnnotations;

namespace BiocomWebApp.Database.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int Age { get; set; }
        public ICollection<Diagnostic> Diagnosticts { get; set; } = new List<Diagnostic>();
    }
}
