using System;
using System.Collections.Generic;

namespace TaxApp.Infrastructure.DataAccess.Models
{
    public partial class TaxSlabTypeMaster
    {
        public TaxSlabTypeMaster()
        {
            TaxSlabDetails = new HashSet<TaxSlabDetails>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public ICollection<TaxSlabDetails> TaxSlabDetails { get; set; }
    }
}
