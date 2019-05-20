using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Packager.ServiceModel.RolesPolicy
{
    public interface IPrincipalFactory
    {
        string Namespace { get; }

        IPrincipal GetPrincipal(Dictionary<string, string> data);

        bool IsValid(Dictionary<string, string> data);
    }
}
