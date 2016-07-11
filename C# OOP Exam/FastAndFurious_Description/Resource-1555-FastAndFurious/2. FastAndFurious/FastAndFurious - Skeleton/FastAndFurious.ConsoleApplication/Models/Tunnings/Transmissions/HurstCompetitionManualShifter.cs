
namespace FastAndFurious.ConsoleApplication.Models.Tunnings.Turbochargers
{
    using FastAndFurious.ConsoleApplication.Common.Enums;
    using FastAndFurious.ConsoleApplication.Models.Tunnings.Turbochargers.Abstract;

    /// <summary>
    /// 1999, 6000, 20, 0, MidGrade, StockShifter
    /// </summary>
    public class HurstCompetitionManualShifter : Transmission
    {
        public HurstCompetitionManualShifter()
            : base(1999, 6000, 20, 0, TunningGradeType.MidGrade, TransmissionType.StockShifter)
        {
        }
    }
}
