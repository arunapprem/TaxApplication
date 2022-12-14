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
    public class MunicipalityRepository : Repository<Municipality>, IMunicipalityRepository
    {
        public MunicipalityRepository(TaxApplicationDbContext context) : base(context)
        {

        }

       

        public TaxApplicationDbContext taxApplicationDbContext => Context as TaxApplicationDbContext;

    }
}
