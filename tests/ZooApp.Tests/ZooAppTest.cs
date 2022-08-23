using ZooLib;

namespace ZooApp.Tests
{
    public class ZooAppTest
    {
        [Fact]
        public void ShouldBeAbleToAddZoo()
        {
            ZooApp zooApp = new();

            Zoo zoo = new();
            zooApp.AddZoo(zoo);

            Assert.NotEmpty(zooApp.Zoos);
        }

        [Fact]
        public void ShouldBeAbleToRunZooApp()
        {
            ZooApp zooApp = new();
            zooApp.Run();
        }

        [Fact]
        public void ShouldBeAbleToRunMain()
        {
            ZooApp.Main(Array.Empty<string>());
        }
    }
}