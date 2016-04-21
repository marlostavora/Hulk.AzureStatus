using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulk.WebAPI.ViewModel
{
    public class ServiceStatusVM
    {
        public ServiceStatusVM()
        {
            Regions = new List<RegionVM>();
            Services = new List<ServiceVM>(); 
        }

        public List<RegionVM> Regions { get; set; }

        public List<ServiceVM> Services { get; set; }

    }
}
