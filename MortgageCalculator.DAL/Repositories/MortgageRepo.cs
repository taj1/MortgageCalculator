using log4net;
using MortgageCalculator.Contracts.Dto;
using MortgageCalculator.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MortgageCalculator.DAL.Repositories
{
    public class MortgageRepo : IMortgageRepo
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MortgageRepo));

        public List<Mortgage> GetAllMortgages()
        {
            List<Mortgage> result = new List<Mortgage>();

            try
            {
                using (var context = new MortgageData.MortgageDataContext())
                {
                    var mortgages = context.Mortgages.OrderBy(mortgage => mortgage.MortgageType).ThenBy(mortgage => mortgage.InterestRate).ToList();

                    foreach (var mortgage in mortgages)
                    {
                        result.Add(new Mortgage
                        {
                            Name = mortgage.Name,
                            EffectiveStartDate = mortgage.EffectiveStartDate,
                            EffectiveEndDate = mortgage.EffectiveEndDate,
                            CancellationFee = mortgage.CancellationFee,
                            EstablishmentFee = mortgage.EstablishmentFee,
                            InterestRepayment = (InterestRepayment)Enum.Parse(typeof(InterestRepayment), mortgage.InterestRepayment.ToString()),
                            MortgageId = mortgage.MortgageId,
                            MortgageType = (MortgageType)Enum.Parse(typeof(MortgageType), mortgage.MortgageType.ToString()),
                            TermsInMonths = NoOfMonths(mortgage.EffectiveStartDate, mortgage.EffectiveEndDate),
                            InterestRate = mortgage.InterestRate
                        }
                        );
                    }

                    log.Info("Loaded mortgage details from the MortgageData.dll");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + ex.StackTrace);
            }

            return result;
        }

        private int NoOfMonths(DateTime startDate, DateTime endDate)
        {
            return ((endDate.Year - startDate.Year) * 12 + endDate.Month - startDate.Month);
        }
    }
}
