using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxApp.Domain.Service.Interface;
using TaxApp.Infrastructure.DataAccess.Interface;
using System.Linq;
using System.Text.RegularExpressions;
using TaxApplication.Utility;
using System.Globalization;

namespace TaxApp.Domain.Service.Class
{
    public class TaxService :  ITaxService
    {
        private readonly IMunicipalityTaxDetailsRepository _municipalityTaxDetailsRepository;
        private readonly IMunicipalityRepository _municipalityRepository;
        private readonly ITaxSlabTypeRepository _taxSlabTypeRepository;


        public TaxService(IMunicipalityTaxDetailsRepository municipalityTaxDetailsRepository,
                          IMunicipalityRepository municipalityRepository,
                          ITaxSlabTypeRepository taxSlabTypeRepository)
        {
            _municipalityTaxDetailsRepository = municipalityTaxDetailsRepository;
            _municipalityRepository = municipalityRepository;
            _taxSlabTypeRepository = taxSlabTypeRepository;
        }

        public async Task<ResultModel> GetTaxDetailsAsync(string MunicipalityName, string ApplicableDT)
        {
            var task = Task.Run(async () =>
            {
                try
                {
                    //validation start
                    var regexchar = new Regex("^[a-zA-Z ]*$");

                    bool isvalidName = regexchar.Match(MunicipalityName).Success;

                    if (isvalidName == false){
                        return new ResultModel() { Status = false, Result = ResourceString.InvalidName };
                    }
                                       
                    DateTime dt;
                    string[] formats = { "yyyy-MM-dd" };
                    if (!DateTime.TryParseExact(ApplicableDT, formats,
                               System.Globalization.CultureInfo.InvariantCulture,
                               DateTimeStyles.None, out dt)) {

                        return new ResultModel() { Status = false, Result = ResourceString.InvalidDate };
                    }

                    //validation end

                    double? result = null;

                    var objMunicipalityTaxDetails = await _municipalityTaxDetailsRepository.GetMunicipalityTaxDetails(MunicipalityName, Convert.ToDateTime(dt.ToString("dd/MM/yyyy HH:mm:ss")));

                    if (objMunicipalityTaxDetails.Count() > 0){
                        result = objMunicipalityTaxDetails.OrderBy(x => x.TaxSlabTypeId).FirstOrDefault().ApplicableTax;
                    }              
                    else
                    {
                         return new ResultModel() { Status = true, Result = ResourceString.NotFound };
                    }

                    return new ResultModel() { Status = true, Result = result };
                }
                catch(Exception e)
                {
                    return new ResultModel() { Status = false, Result = ResourceString.ErrorMessage }; // custom error handling
                }               

        
            });
            return await task;
        }

        public async Task<ResultModel> AddTaxSlabAsync(string Name,
                                                       string TaxApplicableFrom,
                                                       string TaxApplicableTo,
                                                       string TaxType,
                                                       double ApplicableTax)
        {
            var task = Task.Run(async () =>
            {
                try
                {
                       //validation start
                    var regexchar = new Regex("^[a-zA-Z ]*$");

                    bool isvalidName = regexchar.Match(Name).Success;

                    if (isvalidName == false)
                    {
                        return new ResultModel() { Status = false, Result = ResourceString.InvalidName };
                    }

                    DateTime dtFrom;
                    DateTime dtTo;
                    string[] formats = { "yyyy-MM-dd" };
                    if (!DateTime.TryParseExact(TaxApplicableFrom, formats,
                               System.Globalization.CultureInfo.InvariantCulture,
                               DateTimeStyles.None, out dtFrom))
                    {

                        return new ResultModel() { Status = false, Result = ResourceString.InvalidDate };
                    }

                    if (!DateTime.TryParseExact(TaxApplicableTo, formats,
                               System.Globalization.CultureInfo.InvariantCulture,
                               DateTimeStyles.None, out dtTo))
                    {

                        return new ResultModel() { Status = false, Result = ResourceString.InvalidDate };
                    }

                    //check whether the entered name is valid municipality name
                    var municipality = (await _municipalityRepository.Find(x=>x.Name.ToLower() == Name.ToLower()));

                    if(municipality.Count() == 0 ){
                        return new ResultModel() { Status = false, Result = ResourceString.InvalidMunicipalityName };
                    }

                    //check whether the entered tax type is valid // allowed are Daily, Weekly, Monthly, Yearly

                    var taxType = (await _taxSlabTypeRepository.Find(x => x.Type.ToLower() == TaxType.ToLower()));

                    if (taxType.Count() == 0)
                    {                        
                        return new ResultModel() { Status = false, Result = ResourceString.InvalidTaxType };
                    }

                    //check whether the entered TaxApplicableTo is always greater than TaxApplicableFrom  
                    var noOfDays = (Convert.ToDateTime(TaxApplicableTo) - Convert.ToDateTime(TaxApplicableFrom)).TotalDays;

                    if(noOfDays < 1)
                    {
                        return new ResultModel() { Status = false, Result = ResourceString.InvalidDates };                        
                    }

                    //check whether the date range are valid WRT to tax type.
                    switch (taxType.First().Id)
                    {
                        case 1:
                            if(noOfDays >1)
                            {
                                  return new ResultModel() { Status = false, Result = ResourceString.InvalidTaxTypeandDates };
                            }
                            break;

                        case 2:

                            if (noOfDays > 7)
                            {
                                return new ResultModel() { Status = false, Result = ResourceString.InvalidTaxTypeandDates };
                            }
                            break;

                        case 3:
                            if (noOfDays > 31)
                            {
                                return new ResultModel() { Status = false, Result = ResourceString.InvalidTaxTypeandDates };
                            }
                            break;

                        case 4:
                            if (noOfDays > 365)
                            {
                                return new ResultModel() { Status = false, Result = ResourceString.InvalidTaxTypeandDates };
                            }
                            break;

                    }
                                       

                    //validation end


                    bool saveNewTax = await _municipalityTaxDetailsRepository.AddTaxSlabAsync(Convert.ToDateTime(TaxApplicableFrom),
                                                                                              Convert.ToDateTime(TaxApplicableTo),
                                                                                              taxType.First().Id,
                                                                                              municipality.First().Id,
                                                                                              ApplicableTax);

                   

                    return new ResultModel() { Status = true, Result = saveNewTax };
                }
                catch (Exception e)
                {
                    return new ResultModel() { Status = false, Result = ResourceString.ErrorMessage }; // custom error handling
                }


            });
            return await task;
        }
    }
}
