using Packager.ServiceModel.RolesPolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Packager.ServiceModel.WCF.Impl.Test.Security
{
    public class MyPrincipal : IPrincipal
    {
        public MyPrincipal()
        {
        }

        public MyPrincipal(string fullName, string company, string phone, string[] roles, string identity)
        {
            this.FullName = fullName;
            this.Company = company;
            this.Phone = phone;
            this.Roles = roles;
            this.Identity = new GenericIdentity(identity);
        }

        public string Company { get; set; }

        public string FullName { get; set; }

        public IIdentity Identity { get; private set; }

        public string Phone { get; set; }

        public string[] Roles { get; set; }

        public bool IsInRole(string role)
        {
            string[] roles = role.Split(',');
            foreach (var item in roles)
            {
                if (this.Roles.Any(a => item.Contains(a)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
