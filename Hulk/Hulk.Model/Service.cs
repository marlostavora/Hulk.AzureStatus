using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulk.Model
{
    public class Service
    {
        public Service()
        {
            ServiceStatus = new HashSet<ServiceStatus>();
        }

        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public ICollection<ServiceStatus> ServiceStatus { get; set; }
    }
}
