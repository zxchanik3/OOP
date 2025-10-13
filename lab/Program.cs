using System.Collections.Generic;
using System.Text.Json;
using System;
using System.IO;
using System.Linq;


public class Track
{
    public string Name { get; set; }
    public int RequiredLapCount { get; set; }
    public double TotalLength { get; set; }

    public double StartLineX { get; set; }
    public double StartLineY { get; set; }
    public Track(string name, int lapCount, double length, double startX, double startY)
    {
        Name = name;
        RequiredLapCount = lapCount;
        TotalLength = length;
        StartLineX = startX;
        StartLineY = startY;
    }
}

class Driver
{
    public string Name { get; set; }
    public int Number { get; set; }
    public string Team { get; set; } = "Independent";
    public int Wins { get; set; } = 0;
    public int Races { get; set; } = 0;
    public int Podiums { get; set; } = 0;
    public int Position { get; set; }
    public bool Lock { get; set; }

    public Driver() { }
    public Driver(string name, int number, bool lockStatus)
    {
        Name = name;
        Number = number;
        Lock = lockStatus;
    }
    public void AddRaceResult()
    {
        if (Position == 1)
        {
            Wins++;
        }
        if (Position <= 3)
        {
            Podiums++;
        }
        Races++;
    }
    public void SetTeam(string team)
    {
        Team = team;
    }
}


class GameData
{
    public List<Driver> Drivers { get; private set; } = new();

    public void AddDriver(Driver driver)
    {
        // Перевірка, чи номер вільний
        if (Drivers.Any(d => d.Number == driver.Number))
        {
            Console.WriteLine($"Номер {driver.Number} вже зайнятий.");
            return;
        }

        Drivers.Add(driver);
        Console.WriteLine($"Додано гонщика: {driver.Name}");
    }

    public void RemoveDriver(int number)
    {
        var driver = Drivers.FirstOrDefault(d => d.Number == number);

        if (driver == null)
        {
            Console.WriteLine("Гонщика з таким номером не знайдено.");
            return;
        }

        if (driver.Lock)
        {
            Console.WriteLine($"Неможливо видалити стандартного гонщика ({driver.Name}).");
            return;
        }

        Drivers.Remove(driver);
        Console.WriteLine($"Гонщика {driver.Name} видалено.");
    }

    public void SaveToFile(string path)
    {
        string json = JsonSerializer.Serialize(Drivers, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(path, json);
        Console.WriteLine("Дані збережено.");
    }

    public void LoadFromFile(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("Файл не знайдено.");
            Drivers = new List<Driver>();
            return;
        }

        string json = File.ReadAllText(path);
        Drivers = JsonSerializer.Deserialize<List<Driver>>(json) ?? new List<Driver>();
        Console.WriteLine("Дані завантажено.");
    }
}



class Program
{
    static void Main() //Перевірка роботи GameData та Driver
    {
        string path = "drivers.json";
        GameData gameData = new();

        //Завантаження наявних даних або створення нового списку
        gameData.LoadFromFile(path);
        Console.WriteLine("Створюється базовий список гонщиків...");
        gameData.AddDriver(new Driver("Max Verstappen", 1, true));
        gameData.SaveToFile(path);

        Console.WriteLine("\nСписок гонщиків:");
        foreach (var d in gameData.Drivers)
        {
            Console.WriteLine($"{d.Number}: {d.Name} ({d.Team}) | Wins: {d.Wins}, Races: {d.Races}");
        }

        //Додавання нового гонщика
        Console.WriteLine("\nДодати нового гонщика:");
        Console.Write("Ім'я: ");
        string name = Console.ReadLine();
        Console.Write("Номер: ");
        if (!int.TryParse(Console.ReadLine(), out int number))
        {
            Console.WriteLine("Некоректний номер.");
            return;
        }

        gameData.AddDriver(new Driver(name, number, false));
        gameData.SaveToFile(path);

        Console.WriteLine("\nОновлений список гонщиків:");
        foreach (var d in gameData.Drivers)
        {
            Console.WriteLine($"{d.Number}: {d.Name} (Locked: {d.Lock})");
        }

        //Видалення гонщика зі списку
        Console.WriteLine("\nВведіть номер гонщика для видалення:");
        if (int.TryParse(Console.ReadLine(), out int delNum))
        {
            gameData.RemoveDriver(delNum);
            gameData.SaveToFile(path);
        }
    }
}
