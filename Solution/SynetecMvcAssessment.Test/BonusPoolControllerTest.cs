using System;
using InterviewTestTemplatev2.Controllers;
using InterviewTestTemplatev2.Data;
using InterviewTestTemplatev2.Models;
using InterviewTestTemplatev2.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SynetecMvcAssessment.Test
{
    [TestClass]
    public class BonusPoolControllerTest
    {
        private readonly Mock<IBonusCalculatorService> _bonusCalculatorService;
        private readonly Mock<IHrEmployeesRepository> _hrEmployeeRepository;

        public BonusPoolControllerTest()
        {
            _bonusCalculatorService = new Mock<IBonusCalculatorService>();
            _hrEmployeeRepository = new Mock<IHrEmployeesRepository>();
        }

        [TestMethod]
        public void Index_Uses_GetAll_From_IHrEmployeesRepository_To_Retrieve_All_The_HrEmployees()
        {
            // Arrange
            _hrEmployeeRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(NewHrEmployee());
            var sut = new BonusPoolController(_bonusCalculatorService.Object, _hrEmployeeRepository.Object);
            // Act
            sut.Index();
            // Assert
            _hrEmployeeRepository.Verify(r => r.GetAll(), Times.Once);
        }

        [TestMethod]
        public void Calculate_Uses_GetById_From_IHrEmployeesRepository_To_Retrieve_The_HrEmployee()
        {
            // Arrange
            _hrEmployeeRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(NewHrEmployee());
            var sut = new BonusPoolController(_bonusCalculatorService.Object, _hrEmployeeRepository.Object);
            // Act
            sut.Calculate(NewBonusPoolCalculatorModel());
            // Assert
            _hrEmployeeRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void Calculate_Throws_An_Exception_When_The_Employye_Is_Not_Found()
        {
            // Arrange
            var sut = new BonusPoolController(_bonusCalculatorService.Object, _hrEmployeeRepository.Object);
            // Assert
            Assert.ThrowsException<Exception>(() => sut.Calculate(NewBonusPoolCalculatorModel()));
        }

        [TestMethod]
        public void Calculate_Uses_Calculate_From_IBonusCalculatorService_To_Calculate_The_Bonus()
        {
            // Arrange
            _hrEmployeeRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(NewHrEmployee());
            var sut = new BonusPoolController(_bonusCalculatorService.Object, _hrEmployeeRepository.Object);
            // Act
            sut.Calculate(NewBonusPoolCalculatorModel());
            // Assert
            _bonusCalculatorService.Verify(s => s.Calculate(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        private HrEmployee NewHrEmployee()
        {
            return new HrEmployee
            {
                ID = 1
            };
        }

        private BonusPoolCalculatorModel NewBonusPoolCalculatorModel()
        {
            return new BonusPoolCalculatorModel
            {
                BonusPoolAmount = 100000,
                SelectedEmployeeId = 1
            };
        }
    }
}
