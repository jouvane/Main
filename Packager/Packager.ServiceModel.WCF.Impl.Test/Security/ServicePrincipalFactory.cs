using Packager.ServiceModel.RolesPolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Packager.ServiceModel.WCF.Impl.Test.Security
{
    public class ServicePrincipalFactory : IPrincipalFactory
    {
        public string Namespace
        {
            get
            {
                return null;
            }
        }

        public IPrincipal GetPrincipal(Dictionary<string, string> data)
        {
            MyPrincipal myPrincipal = new MyPrincipal();
            if (data != null)
            {
                myPrincipal = new MyPrincipal(data["FullName"], data["Company"], data["Phone"], null, data["Identity"]);
            }

            return myPrincipal;
        }

        public bool IsValid(Dictionary<string, string> data)
        {
            if (data == null)
            {
                return true;
            }
            else
            {
                string[] keys = new string[] { "FullName", "Phone", "Company", "Identity" };
                foreach (var key in keys)
                {
                    if (!data.ContainsKey(key))
                    {
                        return false;

                    }
                }
            }

            return true;
        }
    }
}
