using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlade.Validation.ValidationHelper
{
    public static partial class ValidationHelper
    {
        public static Validator<T, T> IsSatisfiedBy<T>(this Validator<T, T> validator, ISpecification<T> spec, string message)
        {
            return IsSatisfied(validator, spec.IsSatisfiedBy, message);
        }

        public static Validator<T, T> IsNotSatisfiedBy<T>(this Validator<T, T> validator, ISpecification<T> spec, string message)
        {
            return IsSatisfied(validator, t => !spec.IsSatisfiedBy(t), message);
        }
    }
}
