
namespace FastAndFurious.ConsoleApplication.Models.MotorVehicles.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FastAndFurious.ConsoleApplication.Contracts;
    using FastAndFurious.ConsoleApplication.Common.Exceptions;
    using FastAndFurious.ConsoleApplication.Common.Utils;

    public abstract class MotorVehicle : IMotorVehicle, IWeightable, IValuable
    {
        private decimal price;
        private int weightInGrams;
        private int acceleration;
        private int topSpeed;
        private int id;

        private ICollection<ITunningPart> tuningParts;

        public MotorVehicle(decimal price, int weight, int topSpeed, int acceleration)
        {
            this.Price = price;
            this.Weight = weight;
            this.TopSpeed = topSpeed;
            this.Acceleration = acceleration;

            this.tuningParts = new HashSet<ITunningPart>();

            this.id = DataGenerator.GenerateId();
        }

        public decimal Price
        {
            get
            {
                return this.Price + this.TunningParts.Sum(x => x.Price);
            }

            protected set
            {
                this.price = value;
            }
        }

        public int Weight
        {
            get
            {
                return this.weightInGrams + this.TunningParts.Sum(x => x.Weight);
            }

            protected set
            {
                this.weightInGrams = value;
            }
        }

        public int Acceleration
        {
            get
            {
                return this.acceleration + this.TunningParts.Sum(x => x.Acceleration);
                //return this.acceleration;
            }

            protected set
            {
                this.acceleration = value;
            }
        }

        public int TopSpeed
        {
            get
            {
                return this.topSpeed + this.TunningParts.Sum(x => x.TopSpeed);
                //return this.topSpeed;
            }

            protected set
            {
                this.topSpeed = value;
            }
        }

        public IEnumerable<ITunningPart> TunningParts
        {
            get
            {
                return this.tuningParts;
            }
        }

        public int Id
        {
            get
            {
                return this.id;
            }

            protected set
            {
                this.id = value;
            }
        }

        public void AddTunning(ITunningPart part)
        {
            // TODO: NUll
            if (this.tuningParts.Any(existing => existing.GetType().BaseType == part.GetType().BaseType))
            {
                // TODO : probly incorrect ( 1 part PER type ? )
                throw new TunningDuplicationException("This part has already been installed");
            }

            this.tuningParts.Add(part);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="trackLengthInMeters"></param>
        /// <returns></returns>
        public TimeSpan Race(int trackLengthInMeters)
        {
            var topSpeedInMetersSecDecimal = TypeCaster.IntToDecimal(MetricUnitsConverter.GetMetersPerSecondFrom(this.TopSpeed));
            var accelerationDecimal = TypeCaster.IntToDecimal(this.Acceleration);

            var timeToMaxSpeed = topSpeedInMetersSecDecimal / accelerationDecimal;

            var distanceToMaxSpeed = (accelerationDecimal * (timeToMaxSpeed * timeToMaxSpeed)) / 2m;

            var remainingDistance = trackLengthInMeters - distanceToMaxSpeed;

            var timeForRemainingDistance = remainingDistance / topSpeedInMetersSecDecimal;

            var totalTime = timeForRemainingDistance + timeToMaxSpeed;

            return new TimeSpan();
        }

        public bool RemoveTunning(ITunningPart part)
        {
            var result = this.tuningParts.Contains(part);

            if (result)
            {
                this.tuningParts.Remove(part);
            }

            return result;
        }
    }
}
