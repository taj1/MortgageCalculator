using System.Collections.Generic;
using System.Web.Http;
using MortgageCalculator.Contracts.Dto;
using MortgageCalculator.Contracts.Services;

namespace MortgageCalculator.Api.Controllers
{
    public class MortgageController : ApiController
    {
        IMortgageService _mortgageService;

        public MortgageController(IMortgageService mortgageService)
        {
            _mortgageService = mortgageService;
        }

        // GET: api/Mortgage
        [HttpGet]
        public IEnumerable<Mortgage> Get()
        {
            return _mortgageService.GetAllMortgages();
        }

        // GET: api/Mortgage/GetByType
        // GET: api/Mortgage/GetByType/0
        [HttpGet]
        public IEnumerable<Mortgage> GetByType(int? id = null)
        {
            return _mortgageService.GetByType(id);
        }
    }
}
