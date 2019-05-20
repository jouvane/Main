using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetBlade.Mask
{
    public class JqueryMask : IMask
    {
        private static char[] ValuesAcceptedReplace = new char[] { '0', '9', '#', 'A', 'S' };

        public JqueryMask(string[] maskTemplete)
        {
            this.MaskTemplete = maskTemplete.OrderBy(o => o.Length).ToArray();
        }

        public string[] MaskTemplete { get; set; }

        public string CleanValue(string value)
        {
            value = value ?? string.Empty;
            Regex regExp = new Regex("[0-9]|[S]|[A]", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

            foreach (var mask in this.MaskTemplete)
            {
                string marks = regExp.Replace(mask, string.Empty);
                if (value.Length <= mask.Length)
                {
                    foreach (var mark in marks)
                    {
                        value = value.Replace(mark.ToString(), string.Empty);
                    }

                    return value;
                }
            }

            return value;
        }

        public string Format(string value)
        {
            value = value ?? string.Empty;
            string maskedValue = string.Empty;

            foreach (var mask in this.MaskTemplete)
            {
                string maskClean = this.CleanValue(mask);
                if (maskClean.Length >= value.Length)
                {
                    int indexText = 0;
                    for (int i = 0; i < mask.Length; i++)
                    {
                        if (value.Length > indexText)
                        {
                            if (JqueryMask.ValuesAcceptedReplace.Contains(mask[i]))
                            {
                                maskedValue += value[indexText++];
                            }
                            else
                            {
                                maskedValue += mask[i];
                            }
                        }
                        else if (!JqueryMask.ValuesAcceptedReplace.Contains(mask[i]))
                        {
                            maskedValue += mask[i];
                        }
                    }

                    return maskedValue;
                }
            }

            return value;
        }
    }
}
