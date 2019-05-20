using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NetBlade.Mask.MVC.CustomModelBinders
{
    public class MaskCustomModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            MaskAttribute maskAttribute = this.ExtractMaskAttribute(bindingContext);

            if (maskAttribute != null && maskAttribute.ClearMaskSubmit)
            {
                object value = base.BindModel(controllerContext, bindingContext);
                string newValue = maskAttribute.CleanValue(value.ToString());

                return newValue;
            }

            return base.BindModel(controllerContext, bindingContext);
        }

        protected MaskAttribute ExtractMaskAttribute(ModelBindingContext bindingContext)
        {
            Type holderType = bindingContext.ModelMetadata.ContainerType;
            if (holderType != null)
            {
                PropertyInfo propertyType = holderType.GetProperty(bindingContext.ModelMetadata.PropertyName);
                object[] attributes = propertyType.GetCustomAttributes(typeof(MaskAttribute), true);

                if (attributes != null && attributes.Length == 1)
                {
                    return attributes[0] as MaskAttribute;
                }
            }

            return null;
        }
    }
}
