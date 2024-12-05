using System;
using System.Collections.Generic;

// клас "Дорога"
class Road
{
    public double Length { get; set; }
    public double Width { get; set; }
    public int Lanes { get; set; }
    public int TrafficLevel { get; set; } // 0-100%

    public Road(double length, double width, int lanes, int trafficLevel)
    {
        Length = length;
        Width = width;
        Lanes = lanes;
        TrafficLevel = trafficLevel;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Дорога: Довжина={Length}км, Ширина={Width}м, Смуги={Lanes}, Рівень трафіку={TrafficLevel}%");
    }
}

// інтерфейс "може рухатись"
interface IDriveable
{
    void Move(Road road);
    void Stop();
}

// клас "Транспортний засіб"
abstract class Vehicle : IDriveable
{
    public double Speed { get; set; }
    public double Size { get; set; }
    public string Type { get; set; }

    public Vehicle(double speed, double size, string type)
    {
        Speed = speed;
        Size = size;
        Type = type;
    }

    public virtual void Move(Road road)
    {
        if (road.TrafficLevel > 70)
        {
            Console.WriteLine($"{Type} рухається повільно через трафік.");
        }
        else
        {
            Console.WriteLine($"{Type} рухається зі швидкістю {Speed} км/год.");
        }
    }

    public virtual void Stop()
    {
        Console.WriteLine($"{Type} зупинився.");
    }
}

// клас "Автомобіль"
class Car : Vehicle
{
    public Car(double speed, double size)
        : base(speed, size, "Автомобіль")
    {
    }
}

// клас "Автобус"
class Bus : Vehicle
{
    public int PassengerCapacity { get; set; }

    public Bus(double speed, double size, int passengerCapacity)
        : base(speed, size, "Автобус")
    {
        PassengerCapacity = passengerCapacity;
    }

    public override void Move(Road road)
    {
        Console.WriteLine($"Автобус рухається зі швидкістю {Speed} км/год, перевозить {PassengerCapacity} пасажирів.");
    }
}

// клас "Вантажівка"
class Truck : Vehicle
{
    public double CargoCapacity { get; set; }

    public Truck(double speed, double size, double cargoCapacity)
        : base(speed, size, "Вантажівка")
    {
        CargoCapacity = cargoCapacity;
    }

    public override void Move(Road road)
    {
        Console.WriteLine($"Вантажівка перевозить {CargoCapacity} тонн і рухається зі швидкістю {Speed} км/год.");
    }
}

// клас "Симуляція"
class Simulation
{
    private List<Road> roads = new List<Road>();
    private List<Vehicle> vehicles = new List<Vehicle>();

    public void AddRoad(Road road)
    {
        roads.Add(road);
        Console.WriteLine("Дорогу додано до симуляції.");
    }

    public void AddVehicle(Vehicle vehicle)
    {
        vehicles.Add(vehicle);
        Console.WriteLine($"{vehicle.Type} додано до симуляції.");
    }

    public void Run()
    {
        foreach (var road in roads)
        {
            Console.WriteLine("\n--- Дорога ---");
            road.ShowInfo();

            foreach (var vehicle in vehicles)
            {
                Console.WriteLine();
                vehicle.Move(road);
                vehicle.Stop();
            }
        }
    }

    public void OptimizeTraffic()
    {
        foreach (var road in roads)
        {
            if (road.TrafficLevel > 80)
            {
                Console.WriteLine($"\nОптимізуємо трафік на дорозі довжиною {road.Length} км.");
                road.TrafficLevel -= 20; // умовно знижуємо трафік
                Console.WriteLine($"Рівень трафіку знижено до {road.TrafficLevel}%.");
            }
        }
    }
}

// тестуємо
class Program
{
    static void Main()
    {
        // створюємо симуляцію
        var simulation = new Simulation();

        // додаємо дороги
        var road1 = new Road(5, 10, 3, 90); // дорога з високим трафіком
        var road2 = new Road(2, 8, 2, 50); // дорога з низьким трафіком

        simulation.AddRoad(road1);
        simulation.AddRoad(road2);

        // додаємо транспорт
        var car = new Car(100, 4.5);
        var bus = new Bus(60, 10, 50);
        var truck = new Truck(80, 12, 20);

        simulation.AddVehicle(car);
        simulation.AddVehicle(bus);
        simulation.AddVehicle(truck);

        // запускаємо симуляцію
        Console.WriteLine("\n--- Запуск симуляції ---");
        simulation.Run();

        // оптимізуємо трафік
        Console.WriteLine("\n--- Оптимізація трафіку ---");
        simulation.OptimizeTraffic();

        // знову запускаємо симуляцію після оптимізації
        Console.WriteLine("\n--- Симуляція після оптимізації ---");
        simulation.Run();
    }
}
