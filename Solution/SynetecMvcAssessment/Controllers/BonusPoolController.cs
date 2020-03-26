using InterviewTestTemplatev2.Data;
using InterviewTestTemplatev2.Models;
using InterviewTestTemplatev2.Services;
using System;
using System.Web.Mvc;

namespace InterviewTestTemplatev2.Controllers
{
    public class BonusPoolController : Controller
    {
        private readonly IBonusCalculatorService _bonusCalculatorService;
        private readonly IHrEmployeesRepository _hrEmployeeRepository;

        public BonusPoolController(
            IBonusCalculatorService bonusCalculatorService,
            IHrEmployeesRepository hrEmployeeRepository)
        {
            _bonusCalculatorService = bonusCalculatorService;
            _hrEmployeeRepository = hrEmployeeRepository;
        }

        // GET: BonusPool
        public ActionResult Index()
        {
            BonusPoolCalculatorModel model = new BonusPoolCalculatorModel();

            model.AllEmployees = _hrEmployeeRepository.GetAll();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Calculate(BonusPoolCalculatorModel model)
        {
            //load the details of the selected employee using the ID
            var hrEmployee = GetEmployee(model.SelectedEmployeeId);

            // Get the bonus allocation from BonusCalculatorService
            var bonusAllocation = _bonusCalculatorService.Calculate(
                model.BonusPoolAmount,
                hrEmployee.Salary,
                totalSalary: _hrEmployeeRepository.GetTotalSalaries());

            BonusPoolCalculatorResultModel result = new BonusPoolCalculatorResultModel();
            result.hrEmployee = hrEmployee;
            result.bonusPoolAllocation = bonusAllocation;

            return View(result);
        }

        private HrEmployee GetEmployee(int selectedEmployeeId)
        {
            var employee = _hrEmployeeRepository.GetById(selectedEmployeeId);

            return employee != null
                ? employee
                : throw new Exception($"Employee with id {selectedEmployeeId} not found.");
        }
    }
}