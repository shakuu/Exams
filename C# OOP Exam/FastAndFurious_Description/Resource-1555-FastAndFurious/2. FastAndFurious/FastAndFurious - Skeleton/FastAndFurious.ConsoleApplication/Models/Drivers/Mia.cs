namespace FastAndFurious.ConsoleApplication.Models.Drivers
{
    using FastAndFurious.ConsoleApplication.Models.Drivers.Abstract;
    using FastAndFurious.ConsoleApplication.Common.Enums;

    public class Mia : Driver
    {
        public Mia()
            : base("Mia", GenderType.Female)
        {
        }
    }
}