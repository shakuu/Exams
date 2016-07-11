
namespace FastAndFurious.ConsoleApplication.Models.Tunnings.ExhaustSystems
{
    using FastAndFurious.ConsoleApplication.Models.Tunnings.ExhaustSystems.Abstract;
    using FastAndFurious.ConsoleApplication.Common.Enums;

    /// <summary>
    /// 679, 11500, 8, 32, MidGrade, StainlessSteel
    /// </summary>
    public class RemusExhaust : Exhaust
    {
        public RemusExhaust ()
            : base(679, 11500, 8, 32, TunningGradeType.MidGrade, ExhaustType.StainlessSteel)
        {
        }
    }
}