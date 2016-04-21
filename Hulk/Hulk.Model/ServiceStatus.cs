using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulk.Model
{
    public class ServiceStatus
    {
        public int Id { get; set; }
        public int RegionId { get; set; }
        public int ServiceId { get; set; }

        public Status Status { get; set; }

        public DateTime DateUpdate { get; set; }

        public Region Region { get; set; }
        public Service Service { get; set; }
    }
}
