namespace Dealership.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Common;
    using Common.Enums;
    using Dealership.Contracts;

    internal class Vehicle : IVehicle
    {
        private string make;
        private string model;
        private decimal price;
        private int wheels;

        private IList<IComment> comments;

        public Vehicle(string make, string model, decimal price, VehicleType type)
        {
            this.Make = make;
            this.Model = model;
            this.Price = price;
            this.Type = type;
            this.Wheels = (int)this.Type;
            this.comments = new List<IComment>();
        }

        public string Make
        {
            get
            {
                return this.make;
            }

            protected set
            {
                Validator.ValidateNull(
                    value,
                    "Make cannot be null!");

                this.ValidateStringInput(value, "Make", Constants.MinMakeLength, Constants.MaxMakeLength);

                this.make = value;
            }
        }

        public string Model
        {
            get
            {
                return this.model;
            }

            protected set
            {
                Validator.ValidateNull(
                    value,
                    "Make cannot be null!");

                this.ValidateStringInput(value, "Model", Constants.MinModelLength, Constants.MaxModelLength);

                this.model = value;
            }
        }

        public decimal Price
        {
            get
            {
                return this.price;
            }

            protected set
            {
                Validator.ValidateDecimalRange(
                    value,
                    Constants.MinPrice,
                    Constants.MaxPrice,
                    string.Format(
                        Constants.NumberMustBeBetweenMinAndMax,
                        "Price",
                        Constants.MinPrice,
                        Constants.MaxPrice));

                this.price = value;
            }
        }

        public VehicleType Type
        {
            get;

            protected set;
        }

        /// <summary>
        /// Motorcycle wheels are always 2
        ///  Car wheels are always 4
        ///  Truck wheels are always 8
        /// </summary>
        public virtual int Wheels
        {
            get
            {
                return this.wheels;
            }

            protected set
            {
                if (value != (int)this.Type)
                {
                    throw new ArgumentException(
                        string.Format(
                        "{0} must have {1} number of wheels",
                        this.Type.ToString(),
                        (int)this.Type));
                }

                this.wheels = value;
            }
        }

        public IList<IComment> Comments
        {
            get
            {
                // TODO: Needs to have access ? 
                return this.comments;
            }
        }

        protected void ValidateStringInput(string value, string name, int min, int max)
        {
            var inputLenght = value.Length;
            Validator.ValidateIntRange(
                inputLenght, min, max,
                string.Format(
                    Constants.StringMustBeBetweenMinAndMax,
                        name, min, max));
        }

        protected void ValidateIntInput(int value, string name, int min, int max)
        {
            Validator.ValidateIntRange(
                value, min, max,
                string.Format(
                    Constants.NumberMustBeBetweenMinAndMax,
                        name, min, max));
        }

        /// <summary>
        /// 2 space indent
        /// Make: {Make}
        /// Model: {Model}
        /// Wheels: {Wheels}
        /// Price: ${Price}
        /// --COMMENTS-- 4 spaces
        /// --NO COMMENTS--
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var outputBuilder = new StringBuilder();

            outputBuilder.AppendFormat("  Make: {0}", this.Make);
            outputBuilder.AppendLine();
            outputBuilder.AppendFormat("  Model: {0}", this.Model);
            outputBuilder.AppendLine();
            outputBuilder.AppendFormat("  Wheels: {0}", this.Wheels);
            outputBuilder.AppendLine();
            outputBuilder.AppendFormat("  Price: ${0}", this.Price);

            return outputBuilder.ToString();
        }

        protected string AddCommentsToString(string output)
        {
            var outputBuilder = new StringBuilder();
            outputBuilder.Append(output);

            // Comments
            if (this.comments.Count == 0)
            {
                outputBuilder.AppendLine();
                outputBuilder.Append("    --NO COMMENTS--");
                return outputBuilder.ToString();
            }
            else
            {
                outputBuilder.AppendLine();
                outputBuilder.Append("    --COMMENTS--");

                foreach (var comment in this.comments)
                {
                    outputBuilder.AppendLine();
                    outputBuilder.Append(comment);
                }

                outputBuilder.AppendLine();
                outputBuilder.Append("    --COMMENTS--");
            }

            return outputBuilder.ToString();
        }
    }
}
