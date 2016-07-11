namespace Dealership.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Dealership.Common.Enums;
    using Dealership.Contracts;
    using Common;

    internal class User : IUser
    {
        private string username;
        private string firstName;
        private string lastName;
        string password;
        Role role;

        IList<IVehicle> vehicles;
        IDictionary<IVehicle, IComment> comments;

        public User(string username, string firstName, string lastName, string password, string role = "normal")
        {
            this.Username = username;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Password = password;
            this.Role = ConvertStringToRoleType(role);

            this.vehicles = new List<IVehicle>();
            this.comments = new Dictionary<IVehicle, IComment>();
        }

        public string FirstName
        {
            get
            {
                return this.firstName;
            }

            protected set
            {
                Validator.ValidateIntRange(
                    value.Length,
                    Constants.MinNameLength,
                    Constants.MaxNameLength,
                    string.Format(
                        Constants.StringMustBeBetweenMinAndMax,
                        "Firstname",
                        Constants.MinNameLength,
                        Constants.MaxNameLength));

                this.firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return this.lastName;
            }

            protected set
            {
                Validator.ValidateIntRange(
                   value.Length,
                   Constants.MinNameLength,
                   Constants.MaxNameLength,
                   string.Format(
                       Constants.StringMustBeBetweenMinAndMax,
                       "Lastname",
                       Constants.MinNameLength,
                       Constants.MaxNameLength));

                this.lastName = value;
            }
        }

        public string Username
        {
            get
            {
                return this.username;
            }

            protected set
            {
                Validator.ValidateIntRange(
                    value.Length,
                    Constants.MinNameLength,
                    Constants.MaxNameLength,
                    string.Format(
                        Constants.StringMustBeBetweenMinAndMax,
                        "Username",
                        Constants.MinNameLength,
                        Constants.MaxNameLength));

                Validator.ValidateSymbols(
                    value,
                    Constants.UsernamePattern,
                    string.Format(
                        Constants.InvalidSymbols,
                        "Username"));

                this.username = value;
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }

            protected set
            {
                Validator.ValidateIntRange(
                    value.Length,
                    Constants.MinPasswordLength,
                    Constants.MaxPasswordLength,
                    string.Format(
                        Constants.StringMustBeBetweenMinAndMax,
                        "Password",
                        Constants.MinPasswordLength,
                        Constants.MaxPasswordLength));

                Validator.ValidateSymbols(
                    value,
                    Constants.PasswordPattern,
                    string.Format(
                        Constants.InvalidSymbols,
                        "Password"));

                this.password = value;
            }
        }

        /// <summary>
        /// Convert string to RoleType
        /// </summary>
        public Role Role
        {
            get;

            protected set;
        }

        public IList<IVehicle> Vehicles
        {
            get
            {
                return new List<IVehicle>(this.vehicles);
            }
        }

        public void AddComment(IComment commentToAdd, IVehicle vehicleToAddComment)
        {
            Validator.ValidateNull(commentToAdd, Constants.CommentCannotBeNull);
            Validator.ValidateNull(vehicleToAddComment, Constants.VehicleCannotBeNull);

            commentToAdd.Author = this.Username;
            vehicleToAddComment.Comments.Add(commentToAdd);
        }

        /// <summary>
        /// Adding a vehicle
        ///     If the user is admin he cannot add a vehicle
        ///     If the user is not VIP he cannot add more than 5 vehicles
        /// </summary>
        /// <param name="vehicle"></param>
        public void AddVehicle(IVehicle vehicle)
        {
            var roleAsInt = (int)this.Role;

            Validator.ValidateNull(vehicle, Constants.VehicleCannotBeNull);

            Validator.ValidateIntRange(
                roleAsInt,
                (int)Common.Enums.Role.Normal,
                (int)Common.Enums.Role.VIP,
                Constants.AdminCannotAddVehicles);

            if (this.Role != Common.Enums.Role.VIP)
            {
                Validator.ValidateIntRange(
                    this.vehicles.Count + 1,
                    0,
                    Constants.MaxVehiclesToAdd,
                    string.Format(
                    Constants.NotAnVipUserVehiclesAdd,
                    Constants.MaxVehiclesToAdd));
            }

            this.vehicles.Add(vehicle);
        }

        /// <summary>
        /// --USER {Username}--
        /// 1. {Vehicle type}:
        /// </summary>
        /// <returns></returns>
        public string PrintVehicles()
        {
            var vehicleHeader = "{0}. {1}:";

            var outputBuilder = new StringBuilder();

            outputBuilder.AppendFormat("--USER {0}--", this.Username);

            if (this.vehicles.Count == 0)
            {
                outputBuilder.AppendLine();
                outputBuilder.Append("--NO VEHICLES--");
                return outputBuilder.ToString();
            }

            for (var index = 0; index < this.vehicles.Count; index++)
            {
                outputBuilder.AppendLine();
                outputBuilder.AppendFormat(vehicleHeader, index + 1, vehicles[index].GetType().Name);
                outputBuilder.AppendLine();
                outputBuilder.Append(vehicles[index].ToString());
            }

            return outputBuilder.ToString();
        }

        public void RemoveComment(IComment commentToRemove, IVehicle vehicleToRemoveComment)
        {
            Validator.ValidateNull(commentToRemove, Constants.CommentCannotBeNull);
            Validator.ValidateNull(vehicleToRemoveComment, Constants.VehicleCannotBeNull);

            // TODO: FIx ERROR MESSAGE
            if (commentToRemove.Author == this.Username)
            {
                vehicleToRemoveComment.Comments.Remove(commentToRemove);
            }
            else
            {
                throw new ArgumentException(Constants.YouAreNotTheAuthor);
            }
        }

        public void RemoveVehicle(IVehicle vehicle)
        {
            Validator.ValidateNull(vehicle, Constants.VehicleCannotBeNull);

            this.vehicles.Remove(vehicle);
        }

        private Role ConvertStringToRoleType(string role)
        {
            Role result = Role.Normal;

            switch (role)
            {
                case "VIP":
                    result = Role.VIP;
                    break;
                case "Admin":
                    result = Role.Admin;
                    break;
                default:
                    result = Role.Normal;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Username: {Username}, FullName: {FirstName} {LastName}, Role: {Role}
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var output = "";

            output += string.Format(Constants.UserToString, this.Username, this.FirstName, this.LastName);

            output += string.Format(", Role: {0}", this.Role.ToString());

            return output;
        }
    }
}
