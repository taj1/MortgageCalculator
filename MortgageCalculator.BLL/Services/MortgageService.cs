using log4net;
using MortgageCalculator.Contracts.Dto;
using MortgageCalculator.Contracts.Repositories;
using MortgageCalculator.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace MortgageCalculator.BLL.Services
{
    public class MortgageService : IMortgageService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MortgageService));
        private readonly IMortgageRepo _mortgageRepo;

        public MortgageService(IMortgageRepo mortgageRepo)
        {
            _mortgageRepo = mortgageRepo;
        }

        public List<Mortgage> GetAllMortgages()
        {
            List<Mortgage> mortgages = null;
            try
            {
                mortgages = HttpRuntime.Cache["mortgages"] as List<Mortgage>;

                if (mortgages == null)
                {
                    mortgages = _mortgageRepo.GetAllMortgages();

                    HttpRuntime.Cache.Add("mortgages", mortgages, null, DateTime.Now.AddHours(24), Cache.NoSlidingExpiration, CacheItemPriority.High, new CacheItemRemovedCallback(ItemRemovedCallback));
                    log.Info("Mortgages added to cache.");
                }

            }
            catch (Exception ex)
            {
                log.Error(ex.Message + ex.StackTrace);
            }

            return mortgages;
        }

        public List<Mortgage> GetByType(int? mortgageType)
        {
            var mortgages = HttpRuntime.Cache["mortgages"] as List<Mortgage>;

            return (mortgageType == null) ? mortgages : mortgages.Where(mortgage => (int)mortgage.MortgageType == mortgageType.Value).ToList();
        }

        private void ItemRemovedCallback(String key, Object value, CacheItemRemovedReason reason)
        {
            log.Info("Mortgages removed from cache due to expiration.");

            GetAllMortgages();
        }
    }
}
