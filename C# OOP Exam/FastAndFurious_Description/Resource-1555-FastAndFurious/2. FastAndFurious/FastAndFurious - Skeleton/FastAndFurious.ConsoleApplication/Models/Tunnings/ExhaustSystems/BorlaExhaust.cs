
namespace FastAndFurious.ConsoleApplication.Models.Tunnings.ExhaustSystems
{
    using FastAndFurious.ConsoleApplication.Models.Tunnings.ExhaustSystems.Abstract;
    using FastAndFurious.ConsoleApplication.Common.Enums;

    /// <summary>
    /// 1299, 14600, 12, 40, HighGrade, CeramicCoated
    /// </summary>
    public class BorlaExhaust : Exhaust
    {
        public BorlaExhaust() 
            : base(1299, 14600, 12, 40, TunningGradeType.HighGrade, ExhaustType.CeramicCoated)
        {
        }
    }
}
