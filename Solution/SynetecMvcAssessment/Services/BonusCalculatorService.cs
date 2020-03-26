namespace InterviewTestTemplatev2.Services
{
    #region Interface

    public interface IBonusCalculatorService
    {
        int Calculate(int totalBonusPool, int employeeSalary, int totalSalary);
    }

    #endregion

    public class BonusCalculatorService : IBonusCalculatorService
    {
        /// <summary>
        /// Calculate the bonus of an employee
        /// </summary>
        /// <param name="totalBonusPool">The total bonus allocated</param>
        /// <param name="employeeSalary">The employee's salary</param>
        /// <param name="totalSalary">The total salary of the employees</param>
        /// <returns>The calculated bonus</returns>
        public int Calculate(int totalBonusPool, int employeeSalary, int totalSalary)
        {
            //calculate the bonus allocation for the employee
            var bonusPercentage = (decimal)employeeSalary / (decimal)totalSalary;

            return (int)(bonusPercentage * totalBonusPool);
        }
    }
}