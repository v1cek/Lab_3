using System;
using System.Collections.Generic;

// базовий клас "Живий організм"
class LivingOrganism
{
    public int Energy { get; set; }
    public int Age { get; set; }
    public double Size { get; set; }

    public LivingOrganism(int energy, int age, double size)
    {
        Energy = energy;
        Age = age;
        Size = size;
    }

    public void Grow()
    {
        Size += 0.5;
        Energy -= 10;
        Console.WriteLine("Організм росте: Розмір тепер " + Size + ", Енергія тепер " + Energy);
    }
}

// інтерфейс "може розмножуватись"
interface IReproducible
{
    void Reproduce();
}

// інтерфейс "може полювати"
interface IPredator
{
    void Hunt(LivingOrganism prey);
}

// клас "Тварина"
class Animal : LivingOrganism, IReproducible, IPredator
{
    public string Species { get; set; }

    public Animal(int energy, int age, double size, string species)
        : base(energy, age, size)
    {
        Species = species;
    }

    public void Reproduce()
    {
        Console.WriteLine("Тварина розмножується.");
    }

    public void Hunt(LivingOrganism prey)
    {
        if (prey != null && Energy > 10)
        {
            Energy += 20;
            Console.WriteLine($"{Species} полює і отримує енергію. Енергія тепер: {Energy}");
        }
        else
        {
            Console.WriteLine($"{Species} занадто слабке для полювання.");
        }
    }
}

// клас "Рослина"
class Plant : LivingOrganism, IReproducible
{
    public bool HasFlowers { get; set; }

    public Plant(int energy, int age, double size, bool hasFlowers)
        : base(energy, age, size)
    {
        HasFlowers = hasFlowers;
    }

    public void Reproduce()
    {
        Console.WriteLine("Рослина розмножується насінням.");
    }

    public void Photosynthesis()
    {
        Energy += 15;
        Console.WriteLine("Рослина робить фотосинтез. Енергія тепер: " + Energy);
    }
}

// клас "Мікроорганізм"
class Microorganism : LivingOrganism, IReproducible
{
    public bool IsHarmful { get; set; }

    public Microorganism(int energy, int age, double size, bool isHarmful)
        : base(energy, age, size)
    {
        IsHarmful = isHarmful;
    }

    public void Reproduce()
    {
        Console.WriteLine("Мікроорганізм ділиться.");
    }
}

// клас "Екосистема"
class Ecosystem
{
    private List<LivingOrganism> organisms = new List<LivingOrganism>();

    public void AddOrganism(LivingOrganism organism)
    {
        organisms.Add(organism);
        Console.WriteLine("Новий організм доданий до екосистеми.");
    }

    public void Simulate()
    {
        foreach (var organism in organisms)
        {
            organism.Grow();
            if (organism is IReproducible reproducible)
            {
                reproducible.Reproduce();
            }

            if (organism is IPredator predator)
            {
                var prey = organisms.Find(o => o != organism);
                if (prey != null)
                {
                    predator.Hunt(prey);
                }
            }
        }
    }
}

// тестуємо
class Program
{
    static void Main()
    {
        var eco = new Ecosystem();

        var tiger = new Animal(100, 5, 1.5, "Тигр");
        var grass = new Plant(50, 1, 0.3, true);
        var bacteria = new Microorganism(10, 1, 0.01, false);

        eco.AddOrganism(tiger);
        eco.AddOrganism(grass);
        eco.AddOrganism(bacteria);

        eco.Simulate();
    }
}
