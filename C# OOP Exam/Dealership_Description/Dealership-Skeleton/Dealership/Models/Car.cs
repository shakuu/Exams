namespace Dealership.Models
{
    using System.Text;

    using Dealership.Common;
    using Dealership.Common.Enums;
    using Dealership.Contracts;

    internal class Car : Vehicle, ICar
    {
        private int seats;

        public Car(string make, string model, decimal price, int seats)
            : base(make, model, price, VehicleType.Car)
        {
            this.Seats = seats;
        }

        public int Seats
        {
            get
            {
                return this.seats;
            }

            protected set
            {
                //Validator.ValidateIntRange(
                //    value,
                //    Constants.MinSeats,
                //    Constants.MaxSeats,
                //    string.Format(
                //        Constants.NumberMustBeBetweenMinAndMax,
                //        "Seats",
                //        Constants.MinSeats,
                //        Constants.MaxSeats));

                base.ValidateIntInput(
                    value,
                    "Seats",
                    Constants.MinSeats,
                    Constants.MaxSeats
                    );

                this.seats = value;
            }
        }

        /// <summary>
        /// Category/Weight capacity/Seats: {Category/Weight capacity/Seats}
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var outputBuilder = new StringBuilder();
            outputBuilder.Append(base.ToString());
            outputBuilder.AppendLine();
            outputBuilder.AppendFormat("  Seats: {0}", this.Seats);

            var output = base.AddCommentsToString(outputBuilder.ToString());

            return output;
        }
    }
}
