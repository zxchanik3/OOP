using System;

namespace lab
{
	public class Car
	{
		public string Model { get; set; }
		public string Team { get; set; } = "Independent";
		public int Year { get; set; }
		public int Horsepower { get; set; }
		public int Acceleration { get; set; }
		public int TopSpeed { get; set; }
		public int Weight { get; set; }
		public Tyre Tyres { get; set; }
		public int Speed { get; set; }

		public Car() { }

		public Car(string model, int year, int horsepower, Tyre tyre, int acceleration)
		{
			Model = model;
			Year = year;
			Horsepower = horsepower;
			Acceleration = acceleration;
            Tyres = new Tyre(tyre.Type, tyre.Durability, tyre.GripLevel, tyre.WearRate);
		}

        public void UpdateSpeed()
        {

        }

	}
}