using NetBlade.Validation.ClientValidationRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlade.Validation
{
    public class Predicate
    {
        public string ValidationMessage { get; private set; }

        public ClientValidationRule ClienteValidationRule { get; set; }

        public Predicate(string validationMessage)
        {
            this.ValidationMessage = validationMessage;
        }

        public Predicate(string validationMessage, ClientValidationRule clientValidationRule)
            : this(validationMessage)
        {
            this.ClienteValidationRule = clientValidationRule;
        }
    }
}
