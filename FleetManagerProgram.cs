using System;
using System.Collections.Generic;

public class Vehicle
{
    private string _make;
    private string _model;
    private int _year;
    private double _mileage;
    private double _lastServiceMileage;

    public string Make { get { return _make; } set { _make = value; } }
    public string Model { get { return _model; } set { _model = value; } }
    public int Year { get { return _year; } set { _year = value; } }
    public double Mileage { get { return _mileage; } }
    public double LastServiceMileage { get { return _lastServiceMileage; } }

    public Vehicle() { }

    public Vehicle(string make, string model, int year, double mileage)
    {
        _make = make;
        _model = model;
        _year = year;
        _mileage = mileage;
        _lastServiceMileage = 0;
    }

    public void AddMileage(double miles)
    {
        if (miles >= 0)
            _mileage += miles;
    }

    public bool NeedsService()
    {
        return (_mileage - _lastServiceMileage) > 10000;
    }

    public void PerformService()
    {
        _lastServiceMileage = _mileage;
    }

    public string GetSummary()
    {
        string status = NeedsService() ? "Needs Service" : "OK";
        return $"{_year} {_make} {_model} - {_mileage} miles - {status}";
    }
}

public class VehicleManager
{
    private List<Vehicle> _vehicles = new List<Vehicle>();

    public void AddVehicle(Vehicle v)
    {
        _vehicles.Add(v);
    }

    public void RemoveVehicle(string model)
    {
        _vehicles.RemoveAll(v => v.Model == model);
    }

    public double GetAverageMileage()
    {
        if (_vehicles.Count == 0)
            return 0;

        double total = 0;
        foreach (Vehicle v in _vehicles)
            total += v.Mileage;

        return total / _vehicles.Count;
    }

    public void DisplayAllVehicles()
    {
        if (_vehicles.Count == 0)
        {
            Console.WriteLine("No vehicles in fleet.");
            return;
        }

        foreach (Vehicle v in _vehicles)
            Console.WriteLine(v.GetSummary());
    }

    public void ServiceAllDue()
    {
        foreach (Vehicle v in _vehicles)
            if (v.NeedsService())
                v.PerformService();
    }
}

class Program
{
    static void Main()
    {
        VehicleManager manager = new VehicleManager();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n--- Fleet Management Menu ---");
            Console.WriteLine("1. Add Vehicle");
            Console.WriteLine("2. Remove Vehicle");
            Console.WriteLine("3. Display Fleet");
            Console.WriteLine("4. Show Average Mileage");
            Console.WriteLine("5. Service Due Vehicles");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter Make: ");
                    string make = Console.ReadLine();

                    Console.Write("Enter Model: ");
                    string model = Console.ReadLine();

                    Console.Write("Enter Year: ");
                    int year = int.Parse(Console.ReadLine());

                    Console.Write("Enter Mileage: ");
                    double mileage = double.Parse(Console.ReadLine());

                    Vehicle newVehicle = new Vehicle(make, model, year, mileage);
                    manager.AddVehicle(newVehicle);

                    Console.WriteLine("Vehicle added.");
                    break;

                case "2":
                    Console.Write("Enter model to remove: ");
                    string removeModel = Console.ReadLine();
                    manager.RemoveVehicle(removeModel);
                    Console.WriteLine("Vehicle removed (if found).");
                    break;

                case "3":
                    manager.DisplayAllVehicles();
                    break;

                case "4":
                    Console.WriteLine("Average Mileage: " + manager.GetAverageMileage());
                    break;

                case "5":
                    manager.ServiceAllDue();
                    Console.WriteLine("All due vehicles serviced.");
                    break;

                case "6":
                    exit = true;
                    Console.WriteLine("Exiting program...");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
}
