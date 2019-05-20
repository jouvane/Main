using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace NetBlade.Mask.MVC.Helpers
{
    public static class LabelMaskHelpers
    {
        public static MvcHtmlString LabelMask<TModel>(this HtmlHelper<TModel> htmlHelper, string name, string value, MaskAttribute mask, object htmlAttributes = null)
        {
            value = mask.Format(value);
            return htmlHelper.Label(name, value, htmlAttributes);
        }

        public static MvcHtmlString LabelMaskFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            MaskAttribute mask = MaskAttribute.ExtractMaskAttribute<TModel, TValue>(expression);
            string value = ExpressionHelper.GetExpressionText(expression);

            return htmlHelper.LabelMask(ExpressionHelper.GetExpressionText(expression), value, mask, htmlAttributes);
        }
    }
}
