
namespace FastAndFurious.ConsoleApplication.Models.Tunnings.Turbochargers
{
    using FastAndFurious.ConsoleApplication.Common.Enums;
    using FastAndFurious.ConsoleApplication.Models.Tunnings.Turbochargers.Abstract;

    /// <summary>
    /// 699, 3900, 10, 85, HighGrade, SequentialTurbo
    /// </summary>
    public class VortexR35SequentialTurbocharger : Turbocharger
    {
        public VortexR35SequentialTurbocharger() 
            : base(699, 3900, 10, 85, TunningGradeType.HighGrade, TurbochargerType.SequentialTurbo)
        {
        }
    }
}
