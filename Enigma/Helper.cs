using System;
using System.Collections.Generic;
using System.Text;

namespace Enigma
{
    public static class Helper
    {
        public static int letterToNumber(char c)
        {
            return Encoding.ASCII.GetBytes(new char[] { c })[0] - 65;
        }

        public static char NumberToLetter(int i)
        {
            return (char)(i + 65);
        }
    }

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
    public interface ICloneable<T>
    {
        T Clone();
    }

    public static class Extensions
    {
        public static T[] Clone<T>(this T[] array) where T : ICloneable<T>
        {
            var newArray = new T[array.Length];
            for (var i = 0; i < array.Length; i++)
                newArray[i] = array[i].Clone();
            return newArray;
        }
        public static IEnumerable<T> Clone<T>(this IEnumerable<T> items) where T : ICloneable<T>
        {
            foreach (var item in items)
                yield return item.Clone();
        }
    }
}
