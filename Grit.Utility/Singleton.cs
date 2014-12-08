using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Utility
{
    public class Singleton<T>
    {
        private static readonly Lazy<Singleton<T>> lazy =
            new Lazy<Singleton<T>>(() => new Singleton<T>());

        public static Singleton<T> Instance { get { return lazy.Value; } }

        private Singleton()
        {
        }
    }
}
