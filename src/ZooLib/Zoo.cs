using ZooLib.Animals;
using ZooLib.Employees;
using ZooLib.Exceptions;

namespace ZooLib
{
    public class Zoo
    {
        public string Location { get; set; } = string.Empty;
        public List<IEmployee> Employees { get; set; } = new List<IEmployee>();
        public List<Enclosure> Enclosures { get; set; } = new List<Enclosure>();

        public Zoo() { }
        public Zoo(string location)
        {
            Location = location;
        }

        public Enclosure AddEnclosure(string name, int squareFeet)
        {
            var enclosure = new Enclosure
            {
                Name = name,
                ParentZoo = this,
                SquareFeet = squareFeet,
            };

            Enclosures.Add(enclosure);

            return enclosure;
        }

        public Enclosure FindAvailableEnclosure(Animal newAnimal)
        {
            foreach (var enclosure in Enclosures)
            {
                int sqFtLeft = enclosure.SquareFeet;
                Animal? notFriendlyAnimal = null;
                foreach (var animal in enclosure.Animals)
                {
                    sqFtLeft -= animal.RequiredSpaceSqFt;
                    if (!newAnimal.IsFriendlyWith(animal) || !animal.IsFriendlyWith(newAnimal))
                    {
                        notFriendlyAnimal = animal;
                        break;
                    }
                }

                if (notFriendlyAnimal == null && sqFtLeft >= newAnimal.RequiredSpaceSqFt)
                {
                    return enclosure;
                }
            }

            throw new NoAvailableEnclosureException("Can't find an available enclosure");
        }

        public void HireEmployee(IEmployee employee)
        {
            foreach (var enclosure in Enclosures)
            {
                foreach (var animal in enclosure.Animals)
                {
                    if (employee is ZooKeeper zooKeeper)
                    {
                        if (zooKeeper.HasAnimalExperience(animal))
                        {
                            Employees.Add(employee);
                            return;
                        }
                    }
                    if (employee is Veterinarian veterinarian)
                    {
                        if (veterinarian.HasAnimalExperience(animal))
                        {
                            Employees.Add(employee);
                            return;
                        }
                    }
                }
            }

            throw new NoNeededExperienceException(
                $"Can't hire an employee " +
                $"({employee.FirstName} {employee.LastName}) " +
                $"without suitable experiences");
        }

        public void FeedAllAnimals()
        {
            List<Animal> animals = new();
            foreach (var enclosure in Enclosures)
            {
                foreach (var animal in enclosure.Animals)
                {
                    animals.Add(animal);
                }
            }

            var zooKeepers = Employees.OfType<ZooKeeper>();
            foreach (var zooKeeper in zooKeepers)
            {
                for (int i = animals.Count - 1; i >= 0; i--)
                {
                    if (zooKeeper.HasAnimalExperience(animals[i]))
                    {
                        zooKeeper.FeedAnimal(animals[i]);
                        animals.RemoveAt(i);
                    }
                }
            }
        }

        public void HealAllAnimals()
        {
            List<Animal> animals = new();
            foreach (var enclosure in Enclosures)
            {
                foreach (var animal in enclosure.Animals)
                {
                    if (animal.IsSick)
                    {
                        animals.Add(animal);
                    }
                }
            }

            var zooKeepers = Employees.OfType<Veterinarian>();
            foreach (var zooKeeper in zooKeepers)
            {
                for (int i = animals.Count - 1; i >= 0; i--)
                {
                    if (zooKeeper.HasAnimalExperience(animals[i]))
                    {
                        zooKeeper.HealAnimal(animals[i]);
                        animals.RemoveAt(i);
                    }
                }
            }
        }
    }
}
