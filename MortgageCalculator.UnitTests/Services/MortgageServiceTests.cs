using Moq;
using MortgageCalculator.BLL.Services;
using MortgageCalculator.Contracts.Repositories;
using MortgageCalculator.Contracts.Services;
using NUnit.Framework;
using System;
using System.Linq;

namespace MortgageCalculator.UnitTests.Services
{
    [TestFixture]
    public class MortgageServiceTests
    {
        private IMortgageService mortgageService;
        private Mock<IMortgageRepo> mortgageRepo;

        [SetUp]
        public void Setup()
        {
            mortgageRepo = new Mock<IMortgageRepo>();
            mortgageRepo.Setup(x => x.GetAllMortgages()).Returns(() => new System.Collections.Generic.List<Contracts.Dto.Mortgage>
            {
                new Contracts.Dto.Mortgage
                {
                    MortgageId = 1,
                    Name = "Fixed Home Loan (Interest Only)",
                    MortgageType = Contracts.Dto.MortgageType.Fixed,
                    InterestRepayment = Contracts.Dto.InterestRepayment.InterestOnly,
                    EffectiveStartDate = DateTime.Now,
                    EffectiveEndDate = DateTime.Now.AddMonths(12),
                    TermsInMonths = 12,
                    CancellationFee = 259.99M,
                    EstablishmentFee = 199.99M,
                    InterestRate = 4.99M
                },
                new Contracts.Dto.Mortgage
                {
                    MortgageId = 2,
                    Name = "Fixed Home Loan (Principal and Interest)",
                    MortgageType = Contracts.Dto.MortgageType.Fixed,
                    InterestRepayment = Contracts.Dto.InterestRepayment.PrincipalAndInterest,
                    EffectiveStartDate = DateTime.Now,
                    EffectiveEndDate = DateTime.Now.AddMonths(12),
                    TermsInMonths = 12,
                    CancellationFee = 259.99M,
                    EstablishmentFee = 199.99M,
                    InterestRate = 4.81M
                },
                new Contracts.Dto.Mortgage
                {
                    MortgageId = 3,
                    Name = "Variable Home Loan (Interest Only)",
                    MortgageType = Contracts.Dto.MortgageType.Variable,
                    InterestRepayment = Contracts.Dto.InterestRepayment.InterestOnly,
                    EffectiveStartDate = DateTime.Now,
                    EffectiveEndDate = DateTime.Now.AddMonths(12),
                    TermsInMonths = 12,
                    CancellationFee = 259.99M,
                    EstablishmentFee = 199.99M,
                    InterestRate = 5.24M
                },
                new Contracts.Dto.Mortgage
                {
                    MortgageId = 4,
                    Name = "Variable Home Loan (Principal and Interest)",
                    MortgageType = Contracts.Dto.MortgageType.Variable,
                    InterestRepayment = Contracts.Dto.InterestRepayment.PrincipalAndInterest,
                    EffectiveStartDate = DateTime.Now,
                    EffectiveEndDate = DateTime.Now.AddMonths(12),
                    TermsInMonths = 12,
                    CancellationFee = 259.99M,
                    EstablishmentFee = 199.99M,
                    InterestRate = 5.12M
                },
                new Contracts.Dto.Mortgage
                {
                    MortgageId = 5,
                    Name = "Variable Investment Loan (Principal and Interest)",
                    MortgageType = Contracts.Dto.MortgageType.Variable,
                    InterestRepayment = Contracts.Dto.InterestRepayment.PrincipalAndInterest,
                    EffectiveStartDate = DateTime.Now,
                    EffectiveEndDate = DateTime.Now.AddMonths(12),
                    TermsInMonths = 12,
                    CancellationFee = 259.99M,
                    EstablishmentFee = 199.99M,
                    InterestRate = 5.99M
                },
                new Contracts.Dto.Mortgage
                {
                    MortgageId = 6,
                    Name = "Variable Investment Loan (Interest Only)",
                    MortgageType = Contracts.Dto.MortgageType.Variable,
                    InterestRepayment = Contracts.Dto.InterestRepayment.InterestOnly,
                    EffectiveStartDate = DateTime.Now,
                    EffectiveEndDate = DateTime.Now.AddMonths(12),
                    TermsInMonths = 12,
                    CancellationFee = 259.99M,
                    EstablishmentFee = 199.99M,
                    InterestRate = 5.39M
                },
                new Contracts.Dto.Mortgage
                {
                    MortgageId = 7,
                    Name = "Fixed Investment Loan (Principal and Interest)",
                    MortgageType = Contracts.Dto.MortgageType.Fixed,
                    InterestRepayment = Contracts.Dto.InterestRepayment.PrincipalAndInterest,
                    EffectiveStartDate = DateTime.Now,
                    EffectiveEndDate = DateTime.Now.AddMonths(12),
                    TermsInMonths = 12,
                    CancellationFee = 259.99M,
                    EstablishmentFee = 199.99M,
                    InterestRate = 5.89M
                },
                new Contracts.Dto.Mortgage
                {
                    MortgageId = 8,
                    Name = "Fixed Investment Loan (Interest Only)",
                    MortgageType = Contracts.Dto.MortgageType.Fixed,
                    InterestRepayment = Contracts.Dto.InterestRepayment.InterestOnly,
                    EffectiveStartDate = DateTime.Now,
                    EffectiveEndDate = DateTime.Now.AddMonths(12),
                    TermsInMonths = 12,
                    CancellationFee = 259.99M,
                    EstablishmentFee = 199.99M,
                    InterestRate = 5.19M
                }
            });

            mortgageService = new MortgageService(mortgageRepo.Object);
        }

        [Test]
        public void ServiceShouldReturnData()
        {
            var mortgages = mortgageService.GetAllMortgages();

            Assert.IsNotNull(mortgages, "Mortgage Data should not be null.");
            Assert.IsTrue(mortgages.Count > 0, "Mortgage Data count should not be zero.");
            Assert.IsTrue(mortgages.Count == 8, "Mortgage Data count should be eight.");
        }

        [Test]
        public void MortgageDataShouldBeFetchedFromCache()
        {
            var mortgages = mortgageService.GetAllMortgages();
            var mortgages1 = mortgageService.GetAllMortgages();
            var mortgages2 = mortgageService.GetAllMortgages();

            mortgageRepo.Verify(x => x.GetAllMortgages(), Times.Once);
        }

        [Test]
        public void MortgageTypeShouldBeFixed()
        {
            var mortgages = mortgageService.GetByType((int)Contracts.Dto.MortgageType.Fixed);

            Assert.IsNotNull(mortgages, "Mortgage Data should not be null.");
            Assert.IsTrue(mortgages.Count == 4, "Mortgage Data count should be four.");
            Assert.IsFalse(mortgages.Any(m => m.MortgageType == Contracts.Dto.MortgageType.Variable), "Mortgage Data should not contain variable mortgage type.");
        }

        [Test]
        public void MortgageTypeShouldBeVariable()
        {
            var mortgages = mortgageService.GetByType((int)Contracts.Dto.MortgageType.Variable);

            Assert.IsNotNull(mortgages, "Mortgage Data should not be null.");
            Assert.IsTrue(mortgages.Count == 4, "Mortgage Data count should be four.");
            Assert.IsFalse(mortgages.Any(m => m.MortgageType == Contracts.Dto.MortgageType.Fixed), "Mortgage Data should not contain fixed mortgage type.");
        }

        [Test]
        public void MortgageTypeShouldBeVariableAndFixed()
        {
            var mortgages = mortgageService.GetByType(null);

            Assert.IsNotNull(mortgages, "Mortgage Data should not be null.");
            Assert.IsTrue(mortgages.Count == 8, "Mortgage Data count should be four.");
            Assert.IsTrue(mortgages.Any(m => m.MortgageType == Contracts.Dto.MortgageType.Fixed), "Mortgage Data should contain fixed mortgage type.");
            Assert.IsTrue(mortgages.Any(m => m.MortgageType == Contracts.Dto.MortgageType.Variable), "Mortgage Data should contain variable mortgage type.");
        }
    }
}
