
namespace FastAndFurious.ConsoleApplication.Models.Tunnings.ExhaustSystems
{
    using FastAndFurious.ConsoleApplication.Models.Tunnings.ExhaustSystems.Abstract;
    using FastAndFurious.ConsoleApplication.Common.Enums;

    /// <summary>
    /// 379, 12800, 5, 25, LowGrade, NickelChromePlated
    /// </summary>
    public class MagnaflowExhaust : Exhaust
    {
        public MagnaflowExhaust()
            : base(379, 12800, 5, 25, TunningGradeType.LowGrade, ExhaustType.NickelChromePlated)
        {
        }
    }
}
