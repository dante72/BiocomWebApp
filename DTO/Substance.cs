namespace BiocomWebApp.DTO
{
    public class Substance
    {
        public string Name { get; set; }
        public double CurrentValue { get; set; }
        public double MinNormValue { get; set; }
        public double? MaxNormValue { get; set; }
    }
}
