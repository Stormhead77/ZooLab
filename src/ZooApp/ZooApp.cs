using ZooLib;
using ZooLib.Animals;
using ZooLib.Animals.Birds;
using ZooLib.Animals.Mammals;
using ZooLib.Animals.Reptiles;
using ZooLib.Employees;
using ZooLib.Exceptions;

namespace ZooApp
{
    public class ZooApp
    {
        public List<Zoo> Zoos = new();

        public void AddZoo(Zoo zoo)
        {
            Zoos.Add(zoo);
        }

        public void Run()
        {
            var zoo = new Zoo { Location = "Canada" };
            AddZoo(zoo);

            zoo.AddEnclosure("Enclosure1", 50);
            zoo.AddEnclosure("Enclosure2", 30);
            zoo.AddEnclosure("Enclosure3", 10);
            zoo.AddEnclosure("Enclosure4", 1070);
            zoo.AddEnclosure("Enclosure5", 2060);

            var animals = new List<Animal>
            {
                new Elephant(),
                new Elephant() { IsSick = true },
                new Bison(),
                new Lion(),
                new Penguin(),
                new Penguin(),
                new Turtle(),
                new Snake(),
                new Bison(),
                new Penguin(),
                new Parrot { IsSick = true },
            };
            foreach (var animal in animals)
            {
                Enclosure? enclosure = null;
                try
                {
                    enclosure = zoo.FindAvailableEnclosure(animal);
                }
                catch (NoAvailableEnclosureException e)
                {
                    Console.WriteLine(e);
                }

                if (enclosure != null)
                {
                    enclosure.AddAnimal(animal);
                }
            }

            var zooKeepers = new List<ZooKeeper>
            {
                new ZooKeeper { FirstName = "Firat", LastName = "Ratliff" },
                new ZooKeeper { FirstName = "Taylah", LastName = "Hopkins" },
                new ZooKeeper { FirstName = "Kasper", LastName = "Dudley" },
                new ZooKeeper { FirstName = "Rosa", LastName = "Hewitt" },
                new ZooKeeper { FirstName = "Gerard", LastName = "Fields" }
            };
            zooKeepers[0].AddAnimalExperience(new Bison());
            zooKeepers[1].AddAnimalExperience(new Elephant());
            zooKeepers[2].AddAnimalExperience(new Lion());
            zooKeepers[3].AddAnimalExperience(new Parrot());
            zooKeepers[4].AddAnimalExperience(new Penguin());
            zooKeepers[0].AddAnimalExperience(new Snake());
            zooKeepers[1].AddAnimalExperience(new Turtle());
            foreach (var zooKeeper in zooKeepers)
            {
                try
                {
                    zoo.HireEmployee(zooKeeper);
                }
                catch (NoNeededExperienceException e)
                {
                    Console.WriteLine(e);
                }
            }

            var veterinarians = new List<Veterinarian>
            {
                new Veterinarian { FirstName = "Firat", LastName = "Ratliff" },
                new Veterinarian { FirstName = "Taylah", LastName = "Hopkins" },
            };
            zooKeepers[0].AddAnimalExperience(new Bison());
            zooKeepers[1].AddAnimalExperience(new Elephant());
            zooKeepers[0].AddAnimalExperience(new Lion());
            zooKeepers[1].AddAnimalExperience(new Parrot());
            zooKeepers[0].AddAnimalExperience(new Penguin());
            zooKeepers[1].AddAnimalExperience(new Snake());
            zooKeepers[0].AddAnimalExperience(new Turtle());
            foreach (var veterinarian in veterinarians)
            {
                try
                {
                    zoo.HireEmployee(veterinarian);
                }
                catch (NoNeededExperienceException e)
                {
                    Console.WriteLine(e);
                }
            }

            zoo.FeedAllAnimals();

            zoo.HealAllAnimals();
        }

        public static void Main(string[] args)
        {
            ZooApp zooApp = new();
            zooApp.Run();
        }
    }
}