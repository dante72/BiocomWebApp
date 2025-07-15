namespace BiocomWebApp.DTO
{
    public class Diagnostic
    {
        public int UserId { get; set; }
        public Supplement[] Supplements { get; set; }
        public Diet[] OldDiets { get; set; }
        public HealthyDiet[] NewDiets { get; set; }
    }
}
