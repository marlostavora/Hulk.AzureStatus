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
    public class ServiceStatusRepository : EFRepository<ServiceStatus>, IServiceStatusRepository
    {
        public ServiceStatusRepository(DbContext context) : base (context)
        {

        }

        public override IQueryable<ServiceStatus> GetAll()
        {
            return DbSet.Include("Region").Include("Service");
        }
    }
}
