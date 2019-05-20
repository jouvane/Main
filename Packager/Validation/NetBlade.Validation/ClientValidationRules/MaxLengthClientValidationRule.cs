using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlade.Validation.ClientValidationRules
{
    /// <summary>
    /// Rule to express the maximum field length.
    /// </summary>
    public class MaxLengthClientValidationRule : ClientValidationRule
    {
        public MaxLengthClientValidationRule(int maxLength)
        {
            this.ValidationType = "length";
            this.ValidationParameters.Add("max", maxLength);
            this.ErrorMessage = string.Format("Resources.TheFieldXCanNotHaveMoreThanYCharacters", "{0}", maxLength);
        }
    }
}
