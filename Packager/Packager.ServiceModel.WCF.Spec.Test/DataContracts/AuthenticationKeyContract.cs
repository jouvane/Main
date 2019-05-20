using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Packager.ServiceModel.WCF.Spec.Test.DataContracts
{
    [DataContract]
    public class AuthenticationKeyContract
    {
        [DataMember]
        public string Key { get; set; }
    }
}
