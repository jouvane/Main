﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlade.Validation.ValidationHelper.PtBr
{
    public static partial class ValidationHelperPtBr
    {
        public static Validator<T, TResult> Maximo<T, TResult>(this Validator<T, TResult> validator, TResult max)
            where TResult : IComparable<TResult>
        {
            return ValidationHelper.Max(validator, max);
        }

        public static Validator<T, TResult> Minimo<T, TResult>(this Validator<T, TResult> validator, TResult min)
            where TResult : IComparable<TResult>
        {
            return ValidationHelper.Min(validator, min);
        }

        public static Validator<T, TResult> Maximo<T, TResult>(this Validator<T, TResult> validator, Func<TResult> max)
            where TResult : IComparable<TResult>
        {
            return ValidationHelper.Max(validator, max);
        }

        public static Validator<T, TResult> Minimo<T, TResult>(this Validator<T, TResult> validator, Func<TResult> min)
            where TResult : IComparable<TResult>
        {
            return ValidationHelper.Min(validator, min);
        }

        public static Validator<T, TResult> MenorQue<T, TResult>(this Validator<T, TResult> validator, Func<TResult> max)
            where TResult : IComparable<TResult>
        {
            return ValidationHelper.LessThan(validator, max);
        }

        public static Validator<T, TResult> MaiorQue<T, TResult>(this Validator<T, TResult> validator, TResult value)
            where TResult : IComparable<TResult>
        {
            return ValidationHelper.GreaterThan(validator, value);
        }

        public static Validator<T, TResult> MaiorQue<T, TResult>(this Validator<T, TResult> validator, Func<TResult> min)
            where TResult : IComparable<TResult>
        {
            return ValidationHelper.GreaterThan(validator, min);
        }

        public static Validator<T, TResult> MenorQueOuIgual<T, TResult>(this Validator<T, TResult> validator, Func<TResult> max)
            where TResult : IComparable<TResult>
        {
            return ValidationHelper.LessThanOrEqual(validator, max);
        }

        public static Validator<T, TResult> MaiorQueOuIgual<T, TResult>(this Validator<T, TResult> validator, Func<TResult> min)
            where TResult : IComparable<TResult>
        {
            return ValidationHelper.GreaterThanOrEqual(validator, min);
        }

        public static Validator<T, TResult> Entre<T, TResult>(this Validator<T, TResult> validator, TResult min, TResult max)
            where TResult : IComparable<TResult>
        {
            return ValidationHelper.Range(validator, min, max);
        }
    }
}
