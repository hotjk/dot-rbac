using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Utility.Security
{
    public static class RandomText
    {
        private const string FRIENDLY_CHARS = "qwertyipadfghjkcbnm346789";
        
        public static string Generate(int count)
        {
            var output = new StringBuilder(10);

            for (int i = 0; i < count; i++)
            {
                var randomIndex = RandomNumber.Next(FRIENDLY_CHARS.Length - 1);
                output.Append(FRIENDLY_CHARS[randomIndex]);
            }

            return output.ToString();
        }

        public static string GenerateSalt(int count)
        {
            return Convert.ToBase64String(Encoding.Unicode.GetBytes(Generate(count)));
        }
    }
}
