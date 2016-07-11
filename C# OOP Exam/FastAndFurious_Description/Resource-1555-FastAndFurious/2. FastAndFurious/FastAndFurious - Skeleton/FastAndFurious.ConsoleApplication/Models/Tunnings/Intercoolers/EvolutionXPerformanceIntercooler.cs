
namespace FastAndFurious.ConsoleApplication.Models.Tunnings.Turbochargers
{
    using FastAndFurious.ConsoleApplication.Common.Enums;
    using FastAndFurious.ConsoleApplication.Models.Tunnings.Turbochargers.Abstract;

    /// <summary>
    /// 499, 4500, -5, 40, HighGrade, AirToLiquidIntercooler
    /// </summary>
    public class EvolutionXPerformanceIntercooler : Intercooler
    {
        public EvolutionXPerformanceIntercooler()
            : base(499, 4500, -5, 40, TunningGradeType.HighGrade, IntercoolerType.AirToLiquidIntercooler)
        {
        }
    }
}
