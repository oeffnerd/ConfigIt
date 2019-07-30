using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigIt
{
    static class InputUtility
    {
        public static bool ParseInt(string value, out int result)
        {
            try
            {
                result = int.Parse(value);
                return false;
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse '{value}'");
                result = - 1;
            }
            return true;
        }
    }
}
