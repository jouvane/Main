using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlade.Validation.ValidationHelper
{
    public static partial class ValidationHelper
    {
        //public static Validator<T, EntityBase<TId>> Required<T, TId>(this Validator<T, EntityBase<TId>> validator)
        //    where TId : IEquatable<TId>
        //{
        //    return IsSatisfied(validator, p =>
        //    {
        //        if (p == null) return false;
        //        return !p.Id.Equals(default(TId));
        //    }, string.Format("Resources.TheFieldXIsRequired", "{0}"), RequiredClientValidationRule.Rule);
        //}

        //public static Validator<T, EntityBase<TId>> IsRequired<T, TId>(this Validator<T, EntityBase<TId>> validator)
        //    where TId : IEquatable<TId>
        //{
        //    return Required(validator);
        //}

        //public static Validator<T, EntityBase<TId>> CanNotBeEmpty<T, TId>(this Validator<T, EntityBase<TId>> validator)
        //    where TId : IEquatable<TId>
        //{
        //    return Required(validator);
        //}
    }
}
