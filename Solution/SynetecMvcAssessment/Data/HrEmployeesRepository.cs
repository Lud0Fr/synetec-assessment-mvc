using System.Collections.Generic;
using System.Linq;

namespace InterviewTestTemplatev2.Data
{
    #region Interface

    public interface IHrEmployeesRepository
    {
        HrEmployee GetById(int id);
        List<HrEmployee> GetAll();
        int GetTotalSalaries();
    }

    #endregion

    public class HrEmployeesRepository : IHrEmployeesRepository
    {
        private readonly MvcInterviewV3Entities1 _dbContext;

        public HrEmployeesRepository(MvcInterviewV3Entities1 dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get the list of HrEmployees
        /// </summary>
        /// <returns></returns>
        public List<HrEmployee> GetAll()
        {
            return _dbContext.HrEmployees.ToList();
        }

        /// <summary>
        /// Get a HrEmployee by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HrEmployee GetById(int id)
        {
            return _dbContext.HrEmployees.SingleOrDefault(e => e.ID == id);
        }

        /// <summary>
        /// Get the total salary of all the employees
        /// </summary>
        /// <returns></returns>
        public int GetTotalSalaries()
        {
            return _dbContext.HrEmployees.Sum(e => e.Salary);
        }
    }
}