using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Packager.ServiceModel.RolesPolicy
{
    public class CombinedRolesPolicy<T> : IAuthorizationPolicy
        where T : IPrincipalFactory
    {
        private T _currentFactoryPrincipal;

        private string _id = Guid.NewGuid().ToString();

        public string Id
        {
            get
            {
                return this._id;
            }
        }

        public ClaimSet Issuer
        {
            get
            {
                return ClaimSet.System;
            }
        }

        protected T CurrentFactoryPrincipal
        {
            get
            {
                if (this._currentFactoryPrincipal == null)
                {
                    this._currentFactoryPrincipal = Activator.CreateInstance<T>();
                }

                return this._currentFactoryPrincipal;
            }
            set
            {
                this._currentFactoryPrincipal = value;
            }
        }

        public bool Evaluate(EvaluationContext context, ref object state)
        {
            Dictionary<string, string> data = this.GetHeaderMessage(this.CurrentFactoryPrincipal.Namespace);
            bool isValid = this.CurrentFactoryPrincipal.IsValid(data);

            if (isValid)
            {
                IPrincipal principal = this.CurrentFactoryPrincipal.GetPrincipal(data);
                if (principal != null)
                {
                    context.Properties["Principal"] = principal;
                }
            }

            return isValid;
        }

        protected Dictionary<string, string> GetHeaderMessage(string ns = null)
        {
            return OperationContextHelper.Current.GetHeaderMessage(ns);
        }
    }
}
