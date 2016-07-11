
namespace FastAndFurious.ConsoleApplication.Models.Tunnings.Turbochargers
{
    using FastAndFurious.ConsoleApplication.Common.Enums;
    using FastAndFurious.ConsoleApplication.Models.Tunnings.Turbochargers.Abstract;

    /// <summary>
    /// 1599, 4799, 15, 0, LowGrade, SemiManualShifter
    /// </summary>
    public class TWMPerformanceTransmission : Transmission
    {
        public TWMPerformanceTransmission()
            : base(1599, 4799, 15, 0, TunningGradeType.LowGrade, TransmissionType.SemiManualShifter)
        {
        }
    }
}
