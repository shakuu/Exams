namespace FastAndFurious.ConsoleApplication.Models.Drivers
{
    using FastAndFurious.ConsoleApplication.Models.Drivers.Abstract;
    using FastAndFurious.ConsoleApplication.Common.Enums;

    public class Jessi : Driver
    {
        public Jessi()
            : base("Jessi", GenderType.Male)
        {
        }
    }
}