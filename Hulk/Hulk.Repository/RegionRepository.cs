using Hulk.Model;
using Hulk.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulk.Repository
{
    public class RegionRepository : EFRepository<Region>, IRegionRepository
    {
        public RegionRepository(DbContext context) : base (context)
        {

        }
    }
}
