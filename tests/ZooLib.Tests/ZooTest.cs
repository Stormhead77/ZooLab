﻿using ZooLib.Animals;
using ZooLib.Animals.Mammals;
using ZooLib.Employees;
using ZooLib.Exceptions;

namespace ZooLib.Tests
{
    public class ZooTest
    {
        [Fact]
        public void ShouldBeAbleToCreateZoo()
        {
            var zoo = new Zoo();

            Assert.NotNull(zoo);
            Assert.NotNull(zoo.Enclosures);
            Assert.IsType<List<Enclosure>>(zoo.Enclosures);
            Assert.Empty(zoo.Enclosures);
            Assert.NotNull(zoo.Employees);
            Assert.IsType<List<IEmployee>>(zoo.Employees);
            Assert.Empty(zoo.Employees);
            Assert.Equal(string.Empty, zoo.Location);
        }

        [Fact]
        public void ShouldBeAbleToCreateZooWithLocation()
        {
            var zoo = new Zoo("Canada");

            Assert.NotNull(zoo);
            Assert.Equal("Canada", zoo.Location);
        }

        [Fact]
        public void ShouldBeAbleToAddEnclosure()
        {
            var zoo = new Zoo();

            zoo.AddEnclosure("Enclosure1", 1000);

            Assert.True(zoo.Enclosures.Count == 1);
            Assert.True(zoo.Enclosures[0].Name == "Enclosure1");
            Assert.Empty(zoo.Enclosures[0].Animals);
            Assert.True(zoo.Enclosures[0].ParentZoo == zoo);
            Assert.True(zoo.Enclosures[0].SquareFeet == 1000);
        }

        [Theory]
        [MemberData(nameof(GenerateZooWithAvailableEnclosure))]
        public void ShouldBeAbleToFindAvailableEnclosure(Zoo zoo, Animal animal)
        {
            var enclosure = zoo.FindAvailableEnclosure(animal);
            Assert.NotNull(enclosure);
            Assert.Contains(enclosure, zoo.Enclosures);

            enclosure.AddAnimal(animal);
        }

        [Theory]
        [MemberData(nameof(GenerateZooWithoutAvailableEnclosure))]
        public void ShouldThrowNoAvailableEnclosureException(Zoo zoo, Animal animal)
        {
            var exception = Assert.Throws<NoAvailableEnclosureException>(() => zoo.FindAvailableEnclosure(animal));
            Assert.Equal("Can't find an available enclosure", exception.Message);
        }

        [Theory]
        [MemberData(nameof(GenerateZooAndSuitableEmployee))]
        public void ShouldBeAbleToHireEmployee(Zoo zoo, IEmployee employee)
        {
            zoo.HireEmployee(employee);

            Assert.Contains(employee, zoo.Employees);
        }

        [Theory]
        [MemberData(nameof(GenerateZooAndNotSuitableEmployee))]
        public void ShouldThrowNoNeededExperienceException(Zoo zoo, IEmployee employee)
        {
            var exception = Assert.Throws<NoNeededExperienceException>(() => zoo.HireEmployee(employee));
            Assert.Equal(
                $"Can't hire an employee " +
                $"({employee.FirstName} {employee.LastName}) " +
                $"without suitable experiences",
                exception.Message);
        }

        [Theory]
        [MemberData(nameof(GenerateZooForFeedAnimals))]
        public void ShouldBeAbleToFeedAllAnimals(Zoo zoo)
        {
            zoo.FeedAllAnimals();

            foreach (var enclosure in zoo.Enclosures)
            {
                foreach (var animal in enclosure.Animals)
                {
                    Assert.True(animal.FeedTimes.Count == 1);
                }
            }
        }

        [Theory]
        [MemberData(nameof(GenerateZooForNotFeedAnimals))]
        public void ShouldNotBeAbleToFeedAllAnimals(Zoo zoo)
        {
            zoo.FeedAllAnimals();

            foreach (var enclosure in zoo.Enclosures)
            {
                foreach (var animal in enclosure.Animals)
                {
                    Assert.Empty(animal.FeedTimes);
                }
            }
        }

        [Theory]
        [MemberData(nameof(GenerateZooForHealAnimals))]
        public void ShouldBeAbleToHealAllAnimals(Zoo zoo)
        {
            zoo.HealAllAnimals();

            foreach (var enclosure in zoo.Enclosures)
            {
                foreach (var animal in enclosure.Animals)
                {
                    Assert.False(animal.IsSick);
                }
            }
        }

        [Theory]
        [MemberData(nameof(GenerateZooForNotHealAnimals))]
        public void ShouldNotBeAbleToHealAllAnimals(Zoo zoo)
        {
            zoo.HealAllAnimals();

            foreach (var enclosure in zoo.Enclosures)
            {
                foreach (var animal in enclosure.Animals)
                {
                    Assert.True(animal.IsSick);
                }
            }
        }

        private static IEnumerable<object[]> GenerateZooWithAvailableEnclosure()
        {
            yield return new object[]
            {
                new Zoo
                {
                    Enclosures = new List<Enclosure>
                    {
                        new Enclosure { Name = "1", SquareFeet = 1000 },
                        new Enclosure { Name = "2", SquareFeet = 999 }
                    }
                },
                new Bison()
            };
            yield return new object[]
            {
                new Zoo
                {
                    Enclosures = new List<Enclosure>
                    {
                        new Enclosure
                        {
                            Name = "1",
                            SquareFeet = 2000,
                            Animals = new List<Animal> { new Bison() }
                        },
                        new Enclosure
                        {
                            Name = "2",
                            SquareFeet = 1999,
                            Animals = new List<Animal> { new Bison() }
                        }
                    }
                },
                new Elephant()
            };
            yield return new object[]
            {
                new Zoo
                {
                    Enclosures = new List<Enclosure>
                    {
                        new Enclosure
                        {
                            Name = "1",
                            SquareFeet = 2000,
                            Animals = new List<Animal> { new Bison() }
                        },
                        new Enclosure
                        {
                            Name = "2",
                            SquareFeet = 2000,
                            Animals = new List<Animal> { new Lion() }
                        }
                    }
                },
                new Elephant()
            };
        }

        private static IEnumerable<object[]> GenerateZooWithoutAvailableEnclosure()
        {
            yield return new object[]
            {
                new Zoo(),
                new Bison()
            };
            yield return new object[]
            {
                new Zoo
                {
                    Enclosures = new List<Enclosure>
                    {
                        new Enclosure { Name = "1", SquareFeet = 999 }
                    }
                },
                new Bison()
            };
            yield return new object[]
            {
                new Zoo
                {
                    Enclosures = new List<Enclosure>
                    {
                        new Enclosure
                        {
                            Name = "1",
                            SquareFeet = 1999,
                            Animals = new List<Animal> { new Bison() }
                        }
                    }
                },
                new Bison()
            };
            yield return new object[]
            {
                new Zoo
                {
                    Enclosures = new List<Enclosure>
                    {
                        new Enclosure
                        {
                            Name = "1",
                            SquareFeet = 2000,
                            Animals = new List<Animal> { new Lion() }
                        }
                    }
                },
                new Bison()
            };
        }

        private static IEnumerable<object[]> GenerateZooAndSuitableEmployee()
        {
            yield return new object[]
            {
                new Zoo()
                {
                    Enclosures = new List<Enclosure>
                    {
                        new Enclosure
                        {
                            Animals = { new Bison() }
                        }
                    }
                },
                new ZooKeeper
                {
                    AnimalExperiences = { "Bison" }
                }
            };
        }

        private static IEnumerable<object[]> GenerateZooAndNotSuitableEmployee()
        {
            yield return new object[]
            {
                new Zoo(),
                new ZooKeeper
                {
                    AnimalExperiences = { "Bison" }
                }
            };
            yield return new object[]
            {
                new Zoo()
                {
                    Enclosures = new List<Enclosure>
                    {
                        new Enclosure
                        {
                            Animals = { new Bison() }
                        }
                    }
                },
                new Veterinarian()
                {
                    AnimalExperiences = { "Elephant" }
                }
            };
            yield return new object[]
            {
                new Zoo()
                {
                    Enclosures = new List<Enclosure>
                    {
                        new Enclosure
                        {
                            Animals = { new Elephant() }
                        },
                        new Enclosure
                        {
                            Animals = { new Lion(), new Lion() }
                        }
                    }
                },
                new ZooKeeper
                {
                    AnimalExperiences = { "Bison", "Parrot" }
                }
            };
        }

        private static IEnumerable<object[]> GenerateZooForFeedAnimals()
        {
            yield return new object[] { new Zoo
            {
                Enclosures = new List<Enclosure>
                {
                    new Enclosure { Animals = new List<Animal>
                    {
                        new Bison(),
                        new Elephant()
                    } },
                    new Enclosure { Animals = new List<Animal>
                    {
                        new Lion(),
                        new Lion()
                    } }
                },
                Employees = new List<IEmployee>
                {
                    new ZooKeeper { AnimalExperiences = new List<string> { "Bison", "Lion" } },
                    new ZooKeeper { AnimalExperiences = new List<string> { "Elephant" } }
                }
            } };
        }

        private static IEnumerable<object[]> GenerateZooForNotFeedAnimals()
        {
            yield return new object[] { new Zoo
            {
                Enclosures = new List<Enclosure>
                {
                    new Enclosure { Animals = new List<Animal>
                    {
                        new Bison(),
                        new Elephant()
                    } },
                    new Enclosure { Animals = new List<Animal>
                    {
                        new Lion(),
                        new Lion()
                    } }
                },
                Employees = new List<IEmployee>
                {
                    new ZooKeeper { AnimalExperiences = new List<string> { "Parrot", "Penguin" } },
                    new ZooKeeper { AnimalExperiences = new List<string> { "Snake" } }
                }
            } };
        }

        private static IEnumerable<object[]> GenerateZooForHealAnimals()
        {
            yield return new object[] { new Zoo
            {
                Enclosures = new List<Enclosure>
                {
                    new Enclosure { Animals = new List<Animal>
                    {
                        new Bison { IsSick = true },
                        new Elephant { IsSick = true }
                    } },
                    new Enclosure { Animals = new List<Animal>
                    {
                        new Lion { IsSick = true },
                        new Lion { IsSick = true }
                    } }
                },
                Employees = new List<IEmployee>
                {
                    new Veterinarian { AnimalExperiences = new List<string> { "Bison", "Lion" } },
                    new Veterinarian { AnimalExperiences = new List<string> { "Elephant" } }
                }
            } };
        }

        private static IEnumerable<object[]> GenerateZooForNotHealAnimals()
        {
            yield return new object[] { new Zoo
            {
                Enclosures = new List<Enclosure>
                {
                    new Enclosure { Animals = new List<Animal>
                    {
                        new Bison { IsSick = true },
                        new Elephant { IsSick = true }
                    } },
                    new Enclosure { Animals = new List<Animal>
                    {
                        new Lion { IsSick = true },
                        new Lion { IsSick = true }
                    } }
                },
                Employees = new List<IEmployee>
                {
                    new Veterinarian { AnimalExperiences = new List<string> { "Parrot", "Penguin" } },
                    new Veterinarian { AnimalExperiences = new List<string> { "Snake" } }
                }
            } };
        }
    }
}
