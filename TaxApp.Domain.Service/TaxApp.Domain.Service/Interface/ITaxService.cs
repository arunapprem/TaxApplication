using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxApplication.Utility;

namespace TaxApp.Domain.Service.Interface
{
    public interface  ITaxService
    {
        Task<ResultModel> GetTaxDetailsAsync(string MunicipalityName, string ApplicableDT);
        Task<ResultModel> AddTaxSlabAsync(string Name,
                                          string TaxApplicableFrom,
                                          string TaxApplicableTo,
                                          string TaxType,
                                          double ApplicableTax);
    }
}
