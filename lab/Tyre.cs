using System;

namespace lab
{
	class Tyre
	{
		public string Type { get; set; }
		public int Durability { get; set; }
		public int GripLevel { get; set; }
		public float WearRate { get; set; }

		public Tyre() { }

		public Tyre(string type, int durability, int gripLevel, float wearRate)
		{
			Type = type;
			Durability = durability;
			GripLevel = gripLevel;
			WearRate = wearRate;
		}

		public void WearDown()
        {
            Durability -= (int)(10 * WearRate);
            if (Durability < 0) Durability = 0;
        }
	}
}
