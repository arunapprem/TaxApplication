using System;
using System.Collections.Generic;

namespace TaxApp.Infrastructure.DataAccess.Models
{
    public partial class Municipality
    {
        public Municipality()
        {
            InverseCountyMaster = new HashSet<Municipality>();
            MunicipalityTaxDetails = new HashSet<MunicipalityTaxDetails>();
        }

        public int Id { get; set; }
        public int CountyMasterId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public Municipality CountyMaster { get; set; }
        public ICollection<Municipality> InverseCountyMaster { get; set; }
        public ICollection<MunicipalityTaxDetails> MunicipalityTaxDetails { get; set; }
    }
}
