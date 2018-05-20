using MortgageCalculator.Contracts.Repositories;
using NUnit.Framework;

namespace MortgageCalculator.UnitTests.Repositories
{
    [TestFixture]
    public class MortgageRepoTests
    {
        private IMortgageRepo mortgageRepo;

        [SetUp]
        public void Setup()
        {
            mortgageRepo = new DAL.Repositories.MortgageRepo();
        }

        [Test]
        public void MortgageDataShouldNotBeNull()
        {
            var mortgages = mortgageRepo.GetAllMortgages();

            Assert.IsNotNull(mortgages, "Mortgage Data should not be null.");
            Assert.IsTrue(mortgages.Count > 0, "Mortgage Data count should not be zero.");
        }
    }
}
