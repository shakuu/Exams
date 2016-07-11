
namespace FastAndFurious.ConsoleApplication.Models.Tunnings.Abstract
{
    using FastAndFurious.ConsoleApplication.Contracts;
    using FastAndFurious.ConsoleApplication.Common.Utils;

    public abstract class AbstractTuningPart : IAccelerateable, ITopSpeed, IWeightable, IValuable, IIdentifiable
    {

        public AbstractTuningPart(decimal price, int weight, int acceleration, int topSpeed)
        {
            this.Price = price;
            this.Weight = weight;
            this.TopSpeed = topSpeed;
            this.Acceleration = acceleration;

            this.Id = DataGenerator.GenerateId();
        }

        public int Acceleration
        {
            get;
            protected set;
        }

        public int Id
        {
            get;
            set;
        }

        public decimal Price
        {
            get;
            protected set;
        }

        public int TopSpeed
        {
            get;
            protected set;
        }

        public int Weight
        {
            get;
            protected set;
        }
    }
}
