using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hulk.Repository.Helpers.DataContracts
{
    [DataContract]
    public class Region
    {
        [DataMember(Name = "RegionID")]
        public int Id { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Status")]
        public string Status { get; set; }
    }
}
