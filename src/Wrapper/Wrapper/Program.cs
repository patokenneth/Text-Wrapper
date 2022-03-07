using System;
using System.Collections.Generic;
using System.IO;

namespace Wrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(string.Format("Enter the full path of the file to be read from: E.g: {0}", @"C:\Users\Public\testinput.txt"));
            string filePath = Console.ReadLine();

            bool valid = true;
            string message = null;

            //check if the filepath exists and if the file is valid
            (valid, message) = Validator.IsFileValid(filePath);

            //if it's not valid, allow user to quit or enter a valid file path
            while (!valid)
            {
                Console.WriteLine(message+"\n");
                Console.WriteLine("If you would like to quit, press 'Y'. Otherwise, enter a valid text file name");
                string reply = Console.ReadLine();
                if (reply.ToString().ToUpper() == "Y")
                {
                    Console.WriteLine("Take care, bye!");
                    return;
                }
                filePath = reply;
                (valid, message) = Validator.IsFileValid(filePath);
            }

            Console.WriteLine(string.Format("Enter the maximum number of characters per line:"));
            string maxLine = Console.ReadLine();

            //check if the max line is a valid positive integer
            bool isMaxLineValid = Validator.IsMaximumLineValid(maxLine);

            //if number is not valid, allow user to quit or enter a valid number
            while (!isMaxLineValid)
            {
                Console.WriteLine("Kindly enter a valid positive integer.\n");
                Console.WriteLine("If you would like to quit, press 'Y'. Otherwise, enter a valid integer");
                string reply = Console.ReadLine();
                if (reply.ToString().ToUpper() == "Y")
                {
                    Console.WriteLine("Take care, bye!");
                    return;
                }
                maxLine = reply;
                isMaxLineValid = Validator.IsMaximumLineValid(maxLine);
            }

            //if everything checks out fine, proceed and wrap the texts.
            try
            {
                //Read all the lines in the file
                string[] AllLines = File.ReadAllLines(filePath);
                IList<string> allWrappedTexts = new List<string>();

                string DirectoryPath = Path.GetDirectoryName(filePath);
                string existingFileName = Path.GetFileName(filePath);
                string newFilePath = Path.Combine(DirectoryPath, "Wrapped" + existingFileName);

                string[] wrappedTexts = null;

                foreach (var line in AllLines)
                {
                    //call the custom wrapper algorithm "WrapText()" on each line
                    string output = line.WrapText(int.Parse(maxLine));
                    wrappedTexts = output.Split("\n");

                    foreach (var wrappedtext in wrappedTexts)
                    {
                        //save all to the list of strings so we can write to file once.
                        allWrappedTexts.Add(wrappedtext);
                    }

                    wrappedTexts = null;
                }

                File.WriteAllLines(newFilePath, allWrappedTexts);

                Console.WriteLine("Successful! You have created a wrapped text file at " + newFilePath + "\n");
                Console.ReadLine();

            }
            catch (Exception ex)
            {
                //log error if logging is enabled
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                Console.ReadLine();

            }
        }
    }
}
