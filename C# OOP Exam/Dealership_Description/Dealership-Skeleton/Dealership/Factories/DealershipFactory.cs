﻿namespace Dealership.Factories
{
    using System;

    using Dealership.Contracts;
    using Dealership.Models;

    public class DealershipFactory : IDealershipFactory
    {
        public IVehicle CreateCar(string make, string model, decimal price, int seats)
        {
            // TODO: Implement this
            return new Car(make, model, price, seats);
        }

        public IVehicle CreateMotorcycle(string make, string model, decimal price, string category)
        {
            // TODO: Implement this
            return new Motorcycle(make, model, price, category);
        }

        public IVehicle CreateTruck(string make, string model, decimal price, int weightCapacity)
        {
            // TODO: Implement this
            return new Truck(make, model, price, weightCapacity);
        }

        public IUser CreateUser(string username, string firstName, string lastName, string password, string role)
        {
            // TODO: Implement this
            return new User(username, firstName, lastName, password, role);
        }

        public IComment CreateComment(string content)
        {
            // TODO: Implement this
            return new Comment(content);

        }
    }
}
