using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wrapper
{
    public class Validator
    {
        public static (bool, string) IsFileValid(string fileLocation)
        {
            if (string.IsNullOrEmpty(fileLocation))
            {
                return (false, "File path cannot be empty\n");
            }
            if (!File.Exists(fileLocation))
            {
                return (false, "File does not exist\n");
            }
            return (true, "\n");
        }

        public static bool IsMaximumLineValid(string maxLine)
        {
            //reject decimal values
            if (maxLine.Contains('.'))
            {
                return false;
            }
            int outputValue;
            bool isNumber = int.TryParse(maxLine, out outputValue);
            if (isNumber && outputValue > 0)
            {
                return true;
            }
            return false;
        }
    }
}
