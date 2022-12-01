using System;
using System.Collections.Generic;

namespace TaxApp.Infrastructure.DataAccess.Models
{
    public partial class MunicipalityTaxDetails
    {
        public int Id { get; set; }
        public int MunicipalityId { get; set; }
        public int TaxSlabDetailsId { get; set; }

        public Municipality Municipality { get; set; }
        public TaxSlabDetails TaxSlabDetails { get; set; }
    }
}
