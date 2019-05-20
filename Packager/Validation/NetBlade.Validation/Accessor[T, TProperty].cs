using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetBlade.Validation
{
    public class Accessor<T, TProperty>
    {
        public Expression<Func<T, TProperty>> ExpressionAccessor { get; protected set; }

        private Func<T, TProperty> compiledAccessor;

        public Func<T, TProperty> CompiledAccessor
        {
            get
            {
                return this.compiledAccessor ?? (this.compiledAccessor = this.ExpressionAccessor.Compile());
            }
        }

        public Accessor(Expression<Func<T, TProperty>> accessor)
        {
            if (accessor == null)
            {
                throw new ArgumentNullException("accessor");
            }

            if (accessor.Body.NodeType != ExpressionType.MemberAccess && accessor.Body.NodeType != ExpressionType.Parameter)
            {
                throw new ArgumentException("accessor should be ExpressionType.MemberAccess or ExpressionType.Parameter");
            }

            this.ExpressionAccessor = accessor;
        }

        public string MemberName
        {
            // TODO: Incluir referencias para o displayname do data annotations
            get
            {
                MemberExpression memberExpression = this.ExpressionAccessor.Body as MemberExpression;
                if (memberExpression != null)
                {
                    return memberExpression.Member.Name;
                }

                return string.Empty;
            }
        }

        public Type MemberType
        {
            get
            {
                if (this.ExpressionAccessor.Body is MemberExpression)
                {
                    return ExpressionAccessor.ReturnType;
                }

                return null;
            }
        }
    }
}
