namespace FastAndFurious.ConsoleApplication.Models.Drivers
{
    using FastAndFurious.ConsoleApplication.Models.Drivers.Abstract;
    using FastAndFurious.ConsoleApplication.Common.Enums;

    public class Vince : Driver
    {
        public Vince()
            : base("Vince", GenderType.Male)
        {
        }
    }
}