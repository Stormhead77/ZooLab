using ZooLib.Animals;
using ZooLib.Utility;

namespace ZooLib.Employees
{
    public class ZooKeeper
    {
        public List<string> AnimalExperiences { get; set; } = new List<string>();

        public void AddAnimalExperience(Animal animal)
        {
            if (!HasAnimalExperience(animal))
            {
                AnimalExperiences.Add(animal.GetType().Name);
            }
        }

        public bool HasAnimalExperience(Animal animal)
        {
            return AnimalExperiences.Contains(animal.GetType().Name);
        }

        public bool FeedAnimal(Animal animal)
        {
            if (animal.FeedTimes.Count >= 2
                && animal.FeedTimes[^1].DateTime.Date == DateTime.Today
                && animal.FeedTimes[^2].DateTime.Date == DateTime.Today)
            {
                return false;
            }

            var food = (Food.Food)Activator.CreateInstance(Type.GetType("ZooLib.Food." + animal.FavoriteFood[0]));
            animal.Feed(food);
            var feedTime = new FeedTime(DateTime.Now, this);
            animal.FeedTimes.Add(feedTime);

            return true;
        }
    }
}
