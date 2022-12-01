using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxApp.Infrastructure.DataAccess.DTO;
using TaxApp.Infrastructure.DataAccess.Interface;
using TaxApp.Infrastructure.DataAccess.Models;
using System.Linq;

namespace TaxApp.Infrastructure.DataAccess.Class
{
    public class MunicipalityTaxDetailsRepository : Repository<MunicipalityTaxDetails>, IMunicipalityTaxDetailsRepository
    {
        public MunicipalityTaxDetailsRepository(TaxApplicationDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<MunicipalityTaxDetailsDTO>> GetMunicipalityTaxDetails(string MunicipalityName, DateTime ApplicableDT)
        {
            var getTask = Task.Run(async () =>
            {
                List<MunicipalityTaxDetailsDTO> municipalityTaxDetailsDTO = new List<MunicipalityTaxDetailsDTO>();
                
                    try
                    {
                        var source = taxApplicationDbContext.MunicipalityTaxDetails.Join(taxApplicationDbContext.TaxSlabDetails,
                                                                                          mtd => mtd.TaxSlabDetailsId,
                                                                                          tsd => tsd.Id,
                                                                                          (mtd, tsd) => new { mtd, tsd }).Where(x => (x.mtd.Municipality.Name == MunicipalityName
                                                                                       && (ApplicableDT >= x.mtd.TaxSlabDetails.ApplicableFrom 
                                                                                       && ApplicableDT <= x.mtd.TaxSlabDetails.ApplicableTo)));                    

                        if (source.Count() > 0)
                            {
                                foreach (var item in source)
                                {
                                    MunicipalityTaxDetailsDTO objmunicipalityTaxDetailsDTO = new MunicipalityTaxDetailsDTO();

                                    objmunicipalityTaxDetailsDTO.ApplicableTax = item.mtd.TaxSlabDetails.ApplicableTax;
                                    objmunicipalityTaxDetailsDTO.TaxSlabTypeId = item.mtd.TaxSlabDetails.TaxSlabTypeId;

                                    municipalityTaxDetailsDTO.Add(objmunicipalityTaxDetailsDTO);
                                }

                            }
                            return municipalityTaxDetailsDTO;
                    }
                    catch(Exception e)
                    {
                        municipalityTaxDetailsDTO = null;
                        return municipalityTaxDetailsDTO;
                    }                                                           
                
            });
            return await getTask;
        }
        
        
        public async Task<bool> AddTaxSlabAsync(DateTime TaxApplicableFrom,
                                                   DateTime TaxApplicableTo,
                                                   int TaxTypeId,
                                                   int municipalityId,
                                                   double ApplicableTax)
        {

            var orgSaveTask = Task.Run(async () =>
            {

                List<MunicipalityTaxDetails> ListMunicipalityTaxDetails = new List<MunicipalityTaxDetails>();

                MunicipalityTaxDetails objMunicipalityTaxDetails = new MunicipalityTaxDetails();

                objMunicipalityTaxDetails.MunicipalityId = municipalityId;
                //objMunicipalityTaxDetails.TaxSlabDetailsId = MunicipalityId;

                TaxSlabDetails objTaxSlabDetails = new TaxSlabDetails();
                objTaxSlabDetails.ApplicableFrom = TaxApplicableFrom;
                objTaxSlabDetails.ApplicableTo = TaxApplicableTo;
                objTaxSlabDetails.TaxSlabTypeId = municipalityId;
                objTaxSlabDetails.ApplicableTax = ApplicableTax;
                ListMunicipalityTaxDetails.Add(objMunicipalityTaxDetails);
                objTaxSlabDetails.MunicipalityTaxDetails = ListMunicipalityTaxDetails;
                               
                taxApplicationDbContext.TaxSlabDetails.Add(objTaxSlabDetails);
                await taxApplicationDbContext.SaveChangesAsync();
                return true;
            });
            
            return await orgSaveTask;
        }

        public TaxApplicationDbContext taxApplicationDbContext => Context as TaxApplicationDbContext;

    }
}
