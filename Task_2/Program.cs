using System;
using System.Collections.Generic;

// базовий клас "Комп'ютер"
class Computer
{
    public string IPAddress { get; set; }
    public int Power { get; set; }
    public string OS { get; set; }

    public Computer(string ipAddress, int power, string os)
    {
        IPAddress = ipAddress;
        Power = power;
        OS = os;
    }

    public virtual void ShowInfo()
    {
        Console.WriteLine($"Комп'ютер: IP={IPAddress}, Потужність={Power}, ОС={OS}");
    }
}

// інтерфейс "може підключатися"
interface IConnectable
{
    void ConnectTo(Computer other);
    void DisconnectFrom(Computer other);
    void SendData(Computer other, string data);
}

// клас "Сервер"
class Server : Computer, IConnectable
{
    public int StorageCapacity { get; set; }

    public Server(string ipAddress, int power, string os, int storageCapacity)
        : base(ipAddress, power, os)
    {
        StorageCapacity = storageCapacity;
    }

    public void ConnectTo(Computer other)
    {
        Console.WriteLine($"Сервер {IPAddress} підключився до {other.IPAddress}.");
    }

    public void DisconnectFrom(Computer other)
    {
        Console.WriteLine($"Сервер {IPAddress} відключився від {other.IPAddress}.");
    }

    public void SendData(Computer other, string data)
    {
        Console.WriteLine($"Сервер {IPAddress} відправив дані '{data}' до {other.IPAddress}.");
    }
}

// клас "Робоча станція"
class Workstation : Computer, IConnectable
{
    public string UserName { get; set; }

    public Workstation(string ipAddress, int power, string os, string userName)
        : base(ipAddress, power, os)
    {
        UserName = userName;
    }

    public void ConnectTo(Computer other)
    {
        Console.WriteLine($"Робоча станція {IPAddress} підключилась до {other.IPAddress}.");
    }

    public void DisconnectFrom(Computer other)
    {
        Console.WriteLine($"Робоча станція {IPAddress} відключилась від {other.IPAddress}.");
    }

    public void SendData(Computer other, string data)
    {
        Console.WriteLine($"Робоча станція {IPAddress} відправила дані '{data}' до {other.IPAddress}.");
    }
}

// клас "Маршрутизатор"
class Router : Computer, IConnectable
{
    public int MaxConnections { get; set; }

    public Router(string ipAddress, int power, string os, int maxConnections)
        : base(ipAddress, power, os)
    {
        MaxConnections = maxConnections;
    }

    public void ConnectTo(Computer other)
    {
        Console.WriteLine($"Маршрутизатор {IPAddress} підключився до {other.IPAddress}.");
    }

    public void DisconnectFrom(Computer other)
    {
        Console.WriteLine($"Маршрутизатор {IPAddress} відключився від {other.IPAddress}.");
    }

    public void SendData(Computer other, string data)
    {
        Console.WriteLine($"Маршрутизатор {IPAddress} переслав дані '{data}' до {other.IPAddress}.");
    }
}

// клас "Мережа"
class Network
{
    private List<Computer> devices = new List<Computer>();

    public void AddDevice(Computer device)
    {
        devices.Add(device);
        Console.WriteLine($"Пристрій з IP {device.IPAddress} додано до мережі.");
    }

    public void Simulate()
    {
        if (devices.Count < 2)
        {
            Console.WriteLine("Недостатньо пристроїв для симуляції.");
            return;
        }

        // з'єднуємо перші два пристрої
        var device1 = devices[0];
        var device2 = devices[1];

        if (device1 is IConnectable connectable1 && device2 is IConnectable)
        {
            connectable1.ConnectTo(device2);
            connectable1.SendData(device2, "Привіт, як справи?");
            connectable1.DisconnectFrom(device2);
        }
        else
        {
            Console.WriteLine("Один з пристроїв не підтримує підключення.");
        }
    }
}

// тестуємо
class Program
{
    static void Main()
    {
        var network = new Network();

        var server = new Server("192.168.1.1", 500, "Linux", 1000);
        var workstation = new Workstation("192.168.1.2", 300, "Windows", "User1");
        var router = new Router("192.168.1.254", 200, "Cisco OS", 10);

        network.AddDevice(server);
        network.AddDevice(workstation);
        network.AddDevice(router);

        network.Simulate();
    }
}
