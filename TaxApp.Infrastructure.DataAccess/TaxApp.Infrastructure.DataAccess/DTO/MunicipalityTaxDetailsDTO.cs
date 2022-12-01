using System;
using System.Collections.Generic;
using System.Text;

namespace TaxApp.Infrastructure.DataAccess.DTO
{
    public class MunicipalityTaxDetailsDTO
    {
        public int TaxSlabTypeId { get; set; }
        public double ApplicableTax { get; set; }

    }
}
