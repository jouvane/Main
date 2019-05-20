using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetBlade.Mask
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class MaskAttribute : Attribute, IMask
    {
        public MaskAttribute(string maskTemplete, bool clearMaskSubmit = true, bool formatInRender = false)
            : this(new string[] { maskTemplete }, clearMaskSubmit, formatInRender)
        {
        }

        public MaskAttribute(string[] maskTemplete, bool clearMaskSubmit = true, bool formatInRender = false)
        {
            this.ClearMaskSubmit = clearMaskSubmit;
            this.FormatInRender = formatInRender;
            this.MaskTemplete = maskTemplete;
            this.Mask = new JqueryMask(this.MaskTemplete);
        }

        public bool ClearMaskSubmit { get; set; }

        public bool FormatInRender { get; set; }

        public string[] MaskTemplete { get; private set; }

        protected IMask Mask { get; set; }

        public static MaskAttribute ExtractMaskAttribute<TModel, TValue>(Expression<Func<TModel, TValue>> expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            object[] masks = memberExpression.Member.GetCustomAttributes(typeof(MaskAttribute), true);
            MaskAttribute mask = null;

            if (masks != null && masks.Length == 1)
            {
                mask = masks.Cast<MaskAttribute>().First();
            }
            else
            {
                throw new ArgumentNullException("MaskAttribute não encontrado.");
            }

            return mask;
        }

        public string CleanValue(string value)
        {
            return this.Mask.CleanValue(value);
        }

        public string Format(string value)
        {
            return this.Mask.Format(value);
        }
    }
}
