using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlade.Validation
{
    public class Validator<T, TResult>
    {
        public Assertion<T, TResult> Assertion { get; set; }

        public Validator(Assertion<T, TResult> assertion)
        {
            this.Assertion = assertion;
        }
    }
}
