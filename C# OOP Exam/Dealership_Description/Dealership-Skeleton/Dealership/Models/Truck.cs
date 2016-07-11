namespace Dealership.Models
{
    using System.Text;

    using Dealership.Common;
    using Dealership.Common.Enums;
    using Dealership.Contracts;

    internal class Truck : Vehicle, ITruck
    {
        private int weightCapacity;

        public Truck(string make, string model, decimal price, int weightCapacity)
            : base(make, model, price, VehicleType.Truck)
        {
            this.WeightCapacity = weightCapacity;
        }

        public int WeightCapacity
        {
            get
            {
                return this.weightCapacity;
            }

            protected set
            {
                base.ValidateIntInput(
                    value,
                    "Weight capacity",
                    Constants.MinCapacity,
                    Constants.MaxCapacity
                    );

                this.weightCapacity = value;
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
            outputBuilder.AppendFormat("  Weight Capacity: {0}t", this.WeightCapacity);

            var output = base.AddCommentsToString(outputBuilder.ToString());

            return output;
        }
    }
}
