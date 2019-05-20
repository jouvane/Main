using Packager.ServiceModel;
using Packager.ServiceModel.WCF.Spec.Test;
using Packager.ServiceModel.WCF.Spec.Test.DataContracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Policy;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IdentityModel.Claims;
using System.Security.Principal;
using Packager.ServiceModel.WCF.Impl.Test.Security;
using System.ServiceModel.Web;

namespace Packager.ServiceModel.WCF.Impl.Test
{
    public class Service1 : IService1
    {
        public string GetAuthenticationFullName()
        {
            MyPrincipal my = System.Threading.Thread.CurrentPrincipal as MyPrincipal;
            return my != null ? my.FullName : null;
        }

        [WebInvoke(Method = "GET")]
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
    }
}
