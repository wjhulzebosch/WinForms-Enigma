using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Enigma
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EnigmaForm());
        }
        public static int[] CharToIntArray(char[] charArray)
        {
            int[] tempArray = new int[26];
            for (int i = 0; i < 26; i++)
            {
                tempArray[i] = Encoding.ASCII.GetBytes(charArray)[i] - 65;
            }
            return tempArray;
        }

        static int letterToNumber(char c)
        {
            return Encoding.ASCII.GetBytes(new char[] { c })[0] - 65;

        }

        public static char NumberToLetter(int i)
        {
            return (char)(i + 65);
        }
    }
}
