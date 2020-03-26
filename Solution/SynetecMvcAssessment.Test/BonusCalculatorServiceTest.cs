using InterviewTestTemplatev2.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SynetecMvcAssessment.Test
{
    [TestClass]
    public class BonusCalculatorServiceTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var sut = new BonusCalculatorService();
            // Act
            var bonus = sut.Calculate(100000, 60000, 654750);
            // Assert
            Assert.AreEqual(9163, bonus);
        }
    }
}
