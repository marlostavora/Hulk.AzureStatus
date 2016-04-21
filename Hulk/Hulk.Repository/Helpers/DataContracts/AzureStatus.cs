using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hulk.Repository.Helpers.DataContracts
{
    [DataContract]
    public class AzureStatus
    {
        [DataMember(Name = "value")]
        public List<Service> Services { get; set; }
    }
}
