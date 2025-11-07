using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    public class Race
    {
        public Track CurrentTrack { get; set; }
     //   public List<Driver> Participants { get; set; }
        public RaceStatus Status { get; set; }
        public Dictionary<string, double> Results { get; set; }

        public Race()
        {
            Status = RaceStatus.NotStarted;
            Results = new Dictionary<string, double>();
        }

        public Race(Track track) : this()
        {
            CurrentTrack = track;
        }

        public void AddParticipant(string name)
        {
            if (!Results.ContainsKey(name))
            {
                Results.Add(name, 0.0);
            }
            else
            {
                Console.WriteLine("Учасник '{name}' вже в гонці.");
            }
        }

        public void StartRace()
        {
            if (Status == RaceStatus.NotStarted)
            {
                Status = RaceStatus.Active;
                Console.WriteLine("Гонка почалася!");
            }
            else
                Console.WriteLine("Гонка вже має статус {0}", Status);
        }

        public void FinishRace()
        {
            if (Status == RaceStatus.Active)
            {
                var random = new Random();
                var keys = new List<string>(Results.Keys);

                for (int i = 0; i < keys.Count; i++)
                {
                    Results[keys[i]] = (random.NextDouble() * 30 + 60) * CurrentTrack.RequiredLapCount;
                }

                Status = RaceStatus.Finished;
                Console.WriteLine("Гонка завершена. Результати зібрано.");
            }
            else
            {
                Console.WriteLine("Гонка не була активною, статус: {Status}.");
            }
        }
    }
}
