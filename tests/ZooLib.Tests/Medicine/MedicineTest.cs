namespace ZooLib.Tests.Medicine
{
    public class MedicineTest
    {
        [Theory]
        [MemberData(nameof(GenerateMedicine))]
        public void ShouldBeAbleToCreateMedicine(ZooLib.Medicine.Medicine medicine)
        {
            Assert.NotNull(medicine);
        }

        private static IEnumerable<object[]> GenerateMedicine()
        {
            yield return new object[] { new ZooLib.Medicine.Antibiotics() };
            yield return new object[] { new ZooLib.Medicine.AntiDepression() };
            yield return new object[] { new ZooLib.Medicine.AntiInflammatory() };
        }
    }
}