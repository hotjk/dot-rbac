using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Pattern.Spec.Test
{
    class Program
    {
        public class Account
        {
            public bool IsValid { get; set; }
            public decimal AvailableBalance { get; set; }
            public decimal FrozenBalances { get; set; }
        }
        static void Main(string[] args)
        {
            var spec = (new ExpressionSpecification<Account>(x => x.IsValid))
                .And(new ExpressionSpecification<Account>(x => x.AvailableBalance >= 1000m)
                .Or(new ExpressionSpecification<Account>(x => x.FrozenBalances >= 1000m)));
                
            Console.WriteLine(spec.IsSatisfiedBy(new Account { IsValid = true, AvailableBalance = 1000, FrozenBalances = 1000 }));
            Console.WriteLine(spec.IsSatisfiedBy(new Account { IsValid = false, AvailableBalance = 1000, FrozenBalances = 1000 }));
            Console.WriteLine(spec.IsSatisfiedBy(new Account { IsValid = true, AvailableBalance = 800, FrozenBalances = 1000 }));
            Console.WriteLine(spec.IsSatisfiedBy(new Account { IsValid = true, AvailableBalance = 800, FrozenBalances = 800 }));
        }
    }
}
