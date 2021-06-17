using System;
using System.Collections.Generic;
using System.Text;

namespace Enigma
{
    public static class ArrayExtensions
    {
        public static void Fill<T>(this T[] originalArray, T with)
        {
            for (int i = 0; i < originalArray.Length; i++)
            {
                originalArray[i] = with;
            }
        }

    }
}
