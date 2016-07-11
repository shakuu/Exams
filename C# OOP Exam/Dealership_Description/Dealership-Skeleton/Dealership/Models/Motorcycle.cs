namespace Dealership.Models
{
    using System.Text;

    using Common.Enums;
    using Dealership.Contracts;
    using Dealership.Common;

    class Motorcycle : Vehicle, IMotorcycle
    {
        private string category;

        public Motorcycle(string make, string model, decimal price, string category)
            : base(make, model, price, VehicleType.Motorcycle)
        {
            this.Category = category;
        }

        public string Category
        {
            get
            {
                return this.category;
            }

            protected set
            {
                base.ValidateStringInput(
                    value,
                    "Category",
                    Constants.MinCategoryLength,
                    Constants.MaxCategoryLength);

                this.category = value;
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
            outputBuilder.AppendFormat("  Category: {0}", this.Category);

            var output = base.AddCommentsToString(outputBuilder.ToString());

            return output;
        }
    }
}
