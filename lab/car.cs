using System;

namespace OOP
{
    class Car
    {
        public string Model { get; }
        public string Team { get; }
        public double EnginePower { get; }
        public double AeroLevel { get; }
        public double Fuel { get; private set; }
        public double TireWear { get; private set; }
        public bool InRace { get; private set; }

        public Car(string model, string team, double enginePower, double aeroLevel)
        {
            Model = model;
            Team = team;
            EnginePower = enginePower;
            AeroLevel = aeroLevel;
            Fuel = 100.0;
            TireWear = 0.0;
            InRace = false;
        }

        public void StartRace() => InRace = true;

        public (double fuelUsed, double wearAdded) Drive(double distance)
        {
            if (!InRace) return (0, 0);

            double fuelUsed = distance * 0.1;
            double wearAdded = distance * 0.05;

            Fuel = Math.Max(0, Fuel - fuelUsed);
            TireWear = Math.Min(100, TireWear + wearAdded);

            return (fuelUsed, wearAdded);
        }

        public void Refuel(double liters) => Fuel = Math.Min(100, Fuel + liters);

        public void ChangeTires() => TireWear = 0;

        public string GetStatus() =>
            $"{Model} ({Team}) | Engine: {EnginePower}, Aero: {AeroLevel}, Fuel: {Fuel:F1}, Tire wear: {TireWear:F1}%";

        ~Car() { }
    }
}

