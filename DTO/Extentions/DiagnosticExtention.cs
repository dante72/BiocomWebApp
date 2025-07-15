using BiocomWebApp.Database;
using System.Linq;

namespace BiocomWebApp.DTO.Extentions
{
    public static class DiagnosticExtention
    {
        public static Database.Entity.Diagnostic Map(this DTO.Diagnostic diagnosticDto, BiocomContext context)
        {
            var diagnostic = new Database.Entity.Diagnostic()
            {
                UserId = diagnosticDto.UserId,
                DateTime = DateTime.UtcNow
            };

            diagnostic.Diets.AddRange(diagnosticDto.NewDiets.Map(context));
            diagnostic.Diets.AddRange(diagnosticDto.OldDiets.Map(context));
            diagnostic.Diets.AddRange(diagnosticDto.Supplements.Map(context));


            return diagnostic;
        }

        public static DTO.Diagnostic Map(this Database.Entity.Diagnostic diagnostic, BiocomContext context)
        {
            var oldDiets = diagnostic.Diets.Where(d => d is not Database.Entity.HealthyDiet and not Database.Entity.Supplement).ToArray();
            var newDiets = diagnostic.Diets.Where(d => d is Database.Entity.HealthyDiet).Select(i => (Database.Entity.HealthyDiet)i).ToArray();
            var supplements = diagnostic.Diets.Where(d => d is Database.Entity.Supplement).Select(i => (Database.Entity.Supplement)i).ToArray();

            var diagnosticDto = new DTO.Diagnostic();
            diagnosticDto.UserId = diagnostic.UserId;
            diagnosticDto.OldDiets = oldDiets.Map(context);
            diagnosticDto.NewDiets = newDiets.Map(context);
            diagnosticDto.Supplements = supplements.Map(context);

            return diagnosticDto;
        }
    }
}
