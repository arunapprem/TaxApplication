using System;
using System.Collections.Generic;

namespace TaxApp.Infrastructure.DataAccess.Models
{
    public partial class TaxSlabDetails
    {
        public TaxSlabDetails()
        {
            MunicipalityTaxDetails = new HashSet<MunicipalityTaxDetails>();
        }

        public int Id { get; set; }
        public DateTime ApplicableFrom { get; set; }
        public DateTime ApplicableTo { get; set; }
        public int TaxSlabTypeId { get; set; }
        public double ApplicableTax { get; set; }

        public TaxSlabTypeMaster TaxSlabType { get; set; }
        public ICollection<MunicipalityTaxDetails> MunicipalityTaxDetails { get; set; }
    }
}
