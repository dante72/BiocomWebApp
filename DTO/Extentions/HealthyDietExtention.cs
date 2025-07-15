using BiocomWebApp.Database.Entity;
using BiocomWebApp.Database;
using Microsoft.EntityFrameworkCore;

namespace BiocomWebApp.DTO.Extentions
{
    public static class HealthyDietExtention
    {
        public static Database.Entity.HealthyDiet[] Map(this DTO.HealthyDiet[] dietsDto, BiocomContext context)
        {
            var diets = new List<Database.Entity.HealthyDiet>();
            foreach (var dietDto in dietsDto)
            {
                var diet = new Database.Entity.HealthyDiet();
                diet.Energy = dietDto.Energy;
                foreach (var substanse in dietDto.Substances)
                {
                    var dietPart = new DietPart()
                    {
                        SubstanceValue = substanse.CurrentValue,
                        Diet = diet
                    };

                    var dbSubstanse = context.Substances.FirstOrDefault(x => x.Name == substanse.Name);
                    if (dbSubstanse != null)
                    {
                        dietPart.SubstanceId = dbSubstanse.Id;
                    }
                    else
                    {
                        dietPart.Substance = substanse.Map();
                    }

                    diet.DietParts.Add(dietPart);
                    context.AddRange(dietPart);
                }
                diets.Add(diet);
            }

            return diets.ToArray();
        }

        public static DTO.HealthyDiet[] Map(this Database.Entity.HealthyDiet[] diets, BiocomContext context)
        {
            var dietDtos = new List<DTO.HealthyDiet>();
            foreach (var diet in diets)
            {
                var dietDto = new DTO.HealthyDiet()
                {
                    Energy = diet.Energy
                };

                var substances = new List<Substance>();

                if (diet.DietParts == null || diet.DietParts.Count == 0)
                {
                    diet.DietParts = context.DietParts
                        .Include(d => d.Substance)
                        .Where(p => p.DietId == diet.Id)
                        .ToArray();
                }

                foreach (var part in diet.DietParts)
                {
                    var substance = new Substance()
                    {
                        CurrentValue = part.SubstanceValue,
                        Name = part.Substance.Name,
                        MinNormValue = part.Substance.MinNormValue,
                        MaxNormValue = part.Substance.MaxNormValue
                    };

                    substances.Add(substance);
                }

                dietDto.Substances = substances.ToArray();
                dietDtos.Add(dietDto);
            }

            return dietDtos.ToArray();
        }
    }
}

