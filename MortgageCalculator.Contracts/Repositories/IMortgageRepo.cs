using MortgageCalculator.Contracts.Dto;
using System.Collections.Generic;

namespace MortgageCalculator.Contracts.Repositories
{
    public interface IMortgageRepo
    {
        List<Mortgage> GetAllMortgages();
    }
}
