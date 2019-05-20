using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace NetBlade.Mask.MVC.Helpers
{
    public static class TextBoxMaskHelpers
    {
        public static MvcHtmlString TextBoxMask<TModel>(this HtmlHelper<TModel> htmlHelper, string name, string value, Expression<Func<TModel, string>> maskExpression, object htmlAttributes = null)
        {
            MaskAttribute mask = MaskAttribute.ExtractMaskAttribute<TModel, string>(maskExpression);
            return htmlHelper.TextBoxMask(name, value, mask.MaskTemplete, mask.FormatInRender, htmlAttributes);
        }

        public static MvcHtmlString TextBoxMask<TModel>(this HtmlHelper<TModel> htmlHelper, string name, string value, string masks, bool formatInRender = false, object htmlAttributes = null)
        {
            return htmlHelper.TextBoxMask(name, value, new string[] { masks }, formatInRender, htmlAttributes);
        }

        public static MvcHtmlString TextBoxMask<TModel>(this HtmlHelper<TModel> htmlHelper, string name, string value, string[] masks, bool formatInRender = false, object htmlAttributes = null)
        {
            MaskAttribute maskAttribute = new MaskAttribute(masks, true, formatInRender);
            IDictionary<string, object> dictionaryHtmlAttributes = new Dictionary<string, object>();

            if (htmlAttributes != null)
            {
                dictionaryHtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            }

            dictionaryHtmlAttributes.Add("data-mask-templete", Json.Encode(maskAttribute.MaskTemplete));
            if (maskAttribute.FormatInRender)
            {
                value = maskAttribute.Format(value);
            }

            return htmlHelper.TextBox(name, value, dictionaryHtmlAttributes);
        }

        public static MvcHtmlString TextBoxMaskFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            object[] masks = memberExpression.Member.GetCustomAttributes(typeof(MaskAttribute), true);
            MaskAttribute mask = MaskAttribute.ExtractMaskAttribute<TModel, TValue>(expression);

            string value = ExpressionHelper.GetExpressionText(expression);
            return htmlHelper.TextBoxMask(ExpressionHelper.GetExpressionText(expression), value, mask.MaskTemplete, mask.FormatInRender, htmlAttributes);
        }
    }
}
