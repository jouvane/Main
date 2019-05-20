﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlade.Validation.ValidationHelper
{
    public static partial class ValidationHelper
    {
        public static Validator<T, TResult> AreEquals<T, TResult>(this Validator<T, TResult> validator, TResult other)
            where TResult : IEquatable<TResult>
        {
            return IsSatisfied(validator, p => p == null || p.Equals(other), string.Format("Resources.TheFieldXMustBeEqualsY", "{0}", other));
        }

        public static Validator<T, TResult> NotEquals<T, TResult>(this Validator<T, TResult> validator, TResult other)
            where TResult : IEquatable<TResult>
        {
            return IsSatisfied(validator, p =>
            {
                if (p == null) return true;
                return !p.Equals(other);
            }, string.Format("Resources.TheFieldXMustBeDifferentThanY", "{0}", other));
        }
    }
}
