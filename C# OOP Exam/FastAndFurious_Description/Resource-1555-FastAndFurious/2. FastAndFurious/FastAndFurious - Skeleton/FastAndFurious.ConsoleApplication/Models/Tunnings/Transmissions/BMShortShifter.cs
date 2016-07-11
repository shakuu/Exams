
namespace FastAndFurious.ConsoleApplication.Models.Tunnings.Turbochargers
{
    using FastAndFurious.ConsoleApplication.Common.Enums;
    using FastAndFurious.ConsoleApplication.Models.Tunnings.Turbochargers.Abstract;

    /// <summary>
    /// 2799, 5700, 28, 0, HighGrade, ManualShortShifter
    /// </summary>
    public class BMShortShifter : Transmission
    {
        public BMShortShifter()
            : base(2799, 5700, 28, 0, TunningGradeType.HighGrade, TransmissionType.ManualShortShifter)
        {
        }
    }
}
