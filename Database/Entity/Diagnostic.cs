using BiocomWebApp.DTO;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiocomWebApp.Database.Entity
{
    public class Diagnostic
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public List<Diet> Diets { get; set; } = new List<Diet>();
        public int UserId { get; set; }
        public User User { get; set; } = null!; 
    }
}
