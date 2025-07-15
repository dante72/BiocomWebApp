using System.ComponentModel.DataAnnotations;

namespace BiocomWebApp.Database.Entity
{
    public class Supplement : Diet
    {
        public string Name { get; set; }
    }
}
