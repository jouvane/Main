using NetBlade.Validation.ClientValidationRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlade.Validation
{
    public class Predicate<TProperty> : Predicate
    {
        public Func<TProperty, bool> EvalPredicate { get; internal set; }

        public Predicate(Func<TProperty, bool> predicate, string validationMessage)
            : base(validationMessage)
        {
            this.EvalPredicate = predicate;
        }

        public Predicate(Func<TProperty, bool> predicate, string validationMessage, ClientValidationRule clientValidationRule)
            : base(validationMessage, clientValidationRule)
        {
            this.EvalPredicate = predicate;
        }
    }
}
