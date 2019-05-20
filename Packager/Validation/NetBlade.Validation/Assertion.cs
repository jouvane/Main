using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlade.Validation
{
    public abstract class Assertion
    {
        public Assertion WhenAssertion { get; set; }

        public abstract IEnumerable<string> AccessorMemberNames { get; }

        public abstract IEnumerable<Predicate> BasePredicates { get; }

        public abstract bool Evaluate(object obj, IList<ValidationResult> results);
    }
}
