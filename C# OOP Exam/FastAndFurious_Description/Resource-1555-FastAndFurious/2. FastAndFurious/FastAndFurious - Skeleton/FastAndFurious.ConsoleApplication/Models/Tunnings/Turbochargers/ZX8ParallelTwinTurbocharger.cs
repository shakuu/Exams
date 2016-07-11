
namespace FastAndFurious.ConsoleApplication.Models.Tunnings.Turbochargers
{
    using FastAndFurious.ConsoleApplication.Common.Enums;
    using FastAndFurious.ConsoleApplication.Models.Tunnings.Turbochargers.Abstract;

    /// <summary>
    /// 799, 3500, 15, 60, HighGrade, TwinTurbo
    /// </summary>
    public class ZX8ParallelTwinTurbocharger : Turbocharger
    {
        public ZX8ParallelTwinTurbocharger()
            : base(799, 3500, 15, 60, TunningGradeType.HighGrade, TurbochargerType.TwinTurbo)
        {
        }
    }
}
