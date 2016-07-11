
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
                return this.weightInGrams;
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
                return this.acceleration;
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
                return this.topSpeed;
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
            if (this.tuningParts.Contains(part))
            {
                // TODO : probly incorrect ( 1 part PER type ? )
                throw new TunningDuplicationException("This part has already been installed");
            }

            this.tuningParts.Add(part);
        }

        /// <summary>
        /// top speed ^2 / 2x Distance -> a
        /// 
        /// sqrt(2xDistance/ a)
        /// http://physics.stackexchange.com/questions/79575/determing-time-to-complete-known-distance-with-constant-acceleration
        /// </summary>
        /// <param name="trackLengthInMeters"></param>
        /// <returns></returns>
        public TimeSpan Race(int trackLengthInMeters)
        {
            // Oohh boy, you shouldn't have missed the PHYSICS class in high school.
            //var topSPeedInMS =
            //    MetricUnitsConverter.GetMetersPerSecondFrom(this.TopSpeed);

            //var a = ((double)this.TopSpeed * (double)this.TopSpeed) / ((double)2 * ((double)trackLengthInMeters));

            //var result = Math.Sqrt((2 * trackLengthInMeters) / a);

            var longResult = 0;

            // Get Distance to max speed

            // Then get the rest 
            var timeToMax =(double) MetricUnitsConverter.GetMetersPerSecondFrom(this.TopSpeed) / (double)this.Acceleration;

            var distanceTraveledTOMaxSpeed = 0.5 * this.acceleration * timeToMax * timeToMax;

            var remainingDistance = trackLengthInMeters - distanceTraveledTOMaxSpeed;

            var timeFOrTheRest = (double)remainingDistance / (double)MetricUnitsConverter.GetMetersPerSecondFrom(this.TopSpeed);

            return new TimeSpan(Convert.ToInt64(timeFOrTheRest+timeToMax));
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
