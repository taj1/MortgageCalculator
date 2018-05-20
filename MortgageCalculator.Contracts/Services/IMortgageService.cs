using MortgageCalculator.Contracts.Dto;
using System.Collections.Generic;

namespace MortgageCalculator.Contracts.Services
{
    public interface IMortgageService
    {
        List<Mortgage> GetAllMortgages();

        List<Mortgage> GetByType(int? mortgageType);
    }
}
