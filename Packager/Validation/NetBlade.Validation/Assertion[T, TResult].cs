using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlade.Validation
{
    public class Assertion<T, TProperty> : Assertion
    {
        public List<Accessor<T, TProperty>> Accessors { get; private set; }

        public List<Predicate<TProperty>> Predicates { get; private set; }

        public override IEnumerable<Predicate> BasePredicates
        {
            get
            {
                return this.Predicates.Select(p => p as Predicate);
            }
        }

        public Assertion()
        {
            this.Accessors = new List<Accessor<T, TProperty>>();
            this.Predicates = new List<Predicate<TProperty>>();
        }

        public override bool Evaluate(object obj, IList<ValidationResult> results)
        {
            if (WhenAssertion != null && !WhenAssertion.Evaluate(obj, null))
            {
                return true;
            }
            else
            {
                bool allPredicatesAreValid = true;

                foreach (var predicate in Predicates)
                {
                    foreach (var accessor in Accessors)
                    {
                        if (!predicate.EvalPredicate(accessor.CompiledAccessor((T)obj)))
                        {
                            if (results != null)
                            {
                                List<string> memberNames = new List<string> { accessor.MemberName };

                                //if (accessor.MemberType != null && accessor.MemberType.IsSubclassOf(typeof(EntityBase)))
                                //    memberNames.Add(string.Format("{0}.Id", accessor.MemberName));

                                results.Add(new ValidationResult(string.Format(predicate.ValidationMessage, "ValidationHelper.ResourceManager.GetString(accessor.MemberName)"), memberNames));
                            }

                            allPredicatesAreValid = false;
                        }
                    }
                }

                return allPredicatesAreValid;
            }
        }

        public override IEnumerable<string> AccessorMemberNames
        {
            get { return this.Accessors.Select(a => a.MemberName); }
        }
    }
}
