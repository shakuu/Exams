namespace FastAndFurious.ConsoleApplication.Models.Drivers
{
    using FastAndFurious.ConsoleApplication.Models.Drivers.Abstract;
    using FastAndFurious.ConsoleApplication.Common.Enums;

    public class LetiSpaghetti : Driver
    {
        public LetiSpaghetti()
            : base("Leti Spaghetti", GenderType.Female)
        {
        }
    }
}
