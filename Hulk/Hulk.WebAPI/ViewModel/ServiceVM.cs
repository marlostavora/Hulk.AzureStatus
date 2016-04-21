using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulk.WebAPI.ViewModel
{
    public class ServiceVM
    {
        public ServiceVM()
        {
            Regions = new List<RegionStatusVM>();       
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<RegionStatusVM> Regions { get; set; }

    }
}
