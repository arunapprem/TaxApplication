using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxApp.Infrastructure.DataAccess.DTO;
using TaxApp.Infrastructure.DataAccess.Models;

namespace TaxApp.Infrastructure.DataAccess.Interface
{
    public interface IMunicipalityTaxDetailsRepository : IRepository<MunicipalityTaxDetails>
    {
        Task<IEnumerable<MunicipalityTaxDetailsDTO>> GetMunicipalityTaxDetails(string MunicipalityName, DateTime ApplicableDT);
        Task<bool> AddTaxSlabAsync(DateTime TaxApplicableFrom,
                                                   DateTime TaxApplicableTo,
                                                   int TaxTypeId,
                                                   int municipalityId,
                                                   double ApplicableTax);
    }
}
