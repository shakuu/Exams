
namespace FastAndFurious.ConsoleApplication.Models.Tunnings.Turbochargers
{
    using FastAndFurious.ConsoleApplication.Common.Enums;
    using FastAndFurious.ConsoleApplication.Models.Tunnings.Turbochargers.Abstract;

    /// <summary>
    /// 289, 5300, 0, 25, MidGrade, ChargeAirIntercooler
    /// </summary>
    public class ViperGenieIntercooler : Intercooler
    {
        public ViperGenieIntercooler()
            : base(289, 5300, 0, 25, TunningGradeType.MidGrade, IntercoolerType.ChargeAirIntercooler)
        {
        }
    }
}
