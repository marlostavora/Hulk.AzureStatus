using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hulk.Repository.Helpers.DataContracts
{
    [DataContract]
    public class Service
    {
        [DataMember(Name = "ID")]
        public int Id { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Status")]
        public string Status { get; set; }

        [DataMember(Name = "Regions")]
        public List<Region> Regions { get; set; }
    }
}
