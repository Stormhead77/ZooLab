using ZooLib.Animals;
using ZooLib.Exceptions;

namespace ZooLib
{
    public class Enclosure
    {
        public string Name { get; set; } = string.Empty;
        public List<Animal> Animals { get; set; } = new List<Animal>();
        public Zoo ParentZoo { get; set; } = new Zoo();
        public int SquareFeet { get; set; } = 0;

        public void AddAnimal(Animal newAnimal)
        {
            int sqFtLeft = SquareFeet;
            Animal? notFriendlyAnimal = null;
            foreach (Animal animal in Animals)
            {
                sqFtLeft -= animal.RequiredSpaceSqFt;
                if (!newAnimal.IsFriendlyWith(animal) || !animal.IsFriendlyWith(newAnimal))
                {
                    notFriendlyAnimal = animal;
                }
            }

            if (notFriendlyAnimal != null)
            {
                throw new NotFriendlyAnimalException(
                    $"Found an animal ({notFriendlyAnimal.GetType().Name}) that is not friendly with new animal ({newAnimal.GetType().Name})");
            }

            if (sqFtLeft < newAnimal.RequiredSpaceSqFt)
            {
                throw new NoAvailableSpaceException(
                    $"Needs {newAnimal.RequiredSpaceSqFt} square feet of free space, but only {sqFtLeft} left");
            }

            Animals.Add(newAnimal);
        }
    }
}
