

namespace FastAndFurious.ConsoleApplication.Models.Drivers.Abstract
{
    using System.Collections.Generic;
    using FastAndFurious.ConsoleApplication.Common.Enums;
    using FastAndFurious.ConsoleApplication.Common.Utils;
    using FastAndFurious.ConsoleApplication.Contracts;

    public abstract class Driver : IDriver
    {
        private ICollection<IMotorVehicle> vehicles = new List<IMotorVehicle>();

        private readonly int id;

        public Driver(string name, GenderType gender)
        {
            this.Name = name;
            this.Gender = gender;
            this.id = DataGenerator.GenerateId();
        }

        public IMotorVehicle ActiveVehicle
        {
            get;
            protected set;
        }

        public GenderType Gender
        {
            get;
            protected set;
        }

        public int Id
        {
            get
            {
                return this.id;
            }
        }

        public string Name
        {
            get;
            protected set;
        }

        public IEnumerable<IMotorVehicle> Vehicles
        {
            get
            {
                return this.vehicles;
            }
            
        }

        public void AddVehicle(IMotorVehicle vehicle)
        {
            this.vehicles.Add(vehicle);
        }
        public bool RemoveVehicle(IMotorVehicle vehicle)
        {
            var result = this.vehicles.Contains(vehicle);

            if (result)
            {
                this.vehicles.Remove(vehicle);
            }

            return result;
        }
        public void SetActiveVehicle(IMotorVehicle vehicle)
        {
            this.ActiveVehicle = vehicle;
        }
    }
}
