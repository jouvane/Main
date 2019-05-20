using NetBlade.Validation.ClientValidationRules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlade.Validation
{
    public interface IAssert
    {
        List<Assertion> Assertions { get; }

        IEnumerable<ValidationResult> Validate(object instanceReference, IList<ValidationResult> validationContext);

        IEnumerable<ClientValidationRule> GetClientValidationData(string propertyName, object entity);
    }
}
