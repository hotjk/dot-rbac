using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.RBAC
{
    public static class BitArrayExtensions
    {
        /// <summary>
        /// BitArray to byte[]
        /// </summary>
        /// <param name="bits"></param>
        /// <returns></returns>
        public static byte[] ToByte(this BitArray bits)
        {
            int length = (bits.Length - 1) / 8 + 1;
            byte[] ret = new byte[length];
            bits.CopyTo(ret, 0);
            return ret;
        }

        /// <summary>
        /// byte[] to BitArray
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static BitArray ToBitArray(this byte[] bytes)
        {
            return new BitArray(bytes);
        }

        /// <summary>
        /// Print BitArray as bit string
        /// </summary>
        /// <param name="bits"></param>
        /// <returns></returns>
        public static string Print(this BitArray bits)
        {
            StringBuilder sb = new StringBuilder(bits.Length);
            for (int i = 0; i < bits.Length; i++)
            {
                sb.Append(bits[i] ? '1' : '0');
            }
            return sb.ToString();
        }

        /// <summary>
        /// Print BitArray as Byte string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string Print(this byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length);
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(Convert.ToString(bytes[i], 16));
            }
            return sb.ToString();
        }
    }
}
