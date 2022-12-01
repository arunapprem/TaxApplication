using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxApp.Application.WebApi.ApplicationModel
{
    public class TaxApplicationModel
    {
        public string Name { get; set; }
        public string TaxApplicableFrom { get; set; }
        public string TaxApplicableTo { get; set; }
        public string TaxType { get; set; }
        public double ApplicableTax { get; set; }
    }

    public class UpdateTaxApplicationModel
    {        
        public double ApplicableTax { get; set; }
    }
}
