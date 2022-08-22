using ZooLib.Employees;

namespace ZooLib.Validators
{
    public interface IHireValidator
    {
        public List<string> ValidateEmployee(IEmployee employee);
    }
}
