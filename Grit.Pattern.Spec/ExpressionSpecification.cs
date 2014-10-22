using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Pattern.Spec
{
    public class ExpressionSpecification<T> : CompositeSpecification<T>
    {
        private Func<T, bool> expression;
        private string UnsatisfiedMessage;
        public ExpressionSpecification(Func<T, bool> expression, string unsatisfiedMessage = null)
        {
            if (expression == null)
                throw new ArgumentNullException();
            else
                this.expression = expression;

            this.UnsatisfiedMessage = unsatisfiedMessage;
        }

        public override bool IsSatisfiedBy(T o)
        {
            return this.expression(o);
        }
    }
}
