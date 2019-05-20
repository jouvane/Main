using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlade.Validation.ValidationHelper.PtBr
{
    public static partial class ValidationHelperPtBr
    {
        public static Validator<T, T> SejaSatisfeitoPor<T>(this Validator<T, T> validator, ISpecification<T> spec, string mensagem)
        {
            return ValidationHelper.IsSatisfiedBy(validator, spec, mensagem);
        }

        public static Validator<T, T> NaoSejaSatisfeitoPor<T>(this Validator<T, T> validator, ISpecification<T> spec, string mensagem)
        {
            return ValidationHelper.IsNotSatisfiedBy(validator, spec, mensagem);
        }
    }
}
