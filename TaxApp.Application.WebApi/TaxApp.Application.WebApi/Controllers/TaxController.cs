using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaxApp.Application.WebApi.ApplicationModel;
using TaxApp.Domain.Service.Interface;
using TaxApplication.Utility;

namespace TaxApp.Application.WebApi.Controllers
{
    
    public class TaxController : ControllerBase
    {
        private readonly ITaxService _taxService;

        public TaxController(ITaxService taxService)
        {
            _taxService = taxService;
        }
        [HttpGet]
        [Route("api/[controller]/MunicipalityName/{MunicipalityName}/ApplicableDT/{ApplicableDT}/GetTaxDetails")]
        public async Task<IActionResult> GetTaxDetails(string MunicipalityName, string ApplicableDT)
        {
            try
            {
                var response = await _taxService.GetTaxDetailsAsync(MunicipalityName, ApplicableDT);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ResourceString.ErrorMessage); // custom error handling
            }
        }

        [HttpPost]
        [Route("api/[controller]/AddTaxDetails")]
        public async Task<IActionResult> AddTaxDetails([FromBody] TaxApplicationModel addTaxApplicationModel)
        {
            try
            {
                var response = await _taxService.AddTaxSlabAsync(addTaxApplicationModel.Name,
                                                                 addTaxApplicationModel.TaxApplicableFrom,
                                                                 addTaxApplicationModel.TaxApplicableTo,
                                                                 addTaxApplicationModel.TaxType,
                                                                 addTaxApplicationModel.ApplicableTax);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ResourceString.ErrorMessage); // custom error handling
            }
        }

        
    }
}
