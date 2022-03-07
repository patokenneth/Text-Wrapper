using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wrapper
{
    public static class TextWrapperAlgo
    {
        //text wrap algorithm is an extension method
        public static string WrapText(this string eachLine, int maxCharPerLine)
        {
            string[] wordArray = eachLine.Split(' ');

            Queue<string> wordQueue = new Queue<string>();

            foreach (string item in wordArray)
            {
                wordQueue.Enqueue(item);
            }

            //create a string builder to temporarily hold wrapped text
            StringBuilder output = new StringBuilder(eachLine.Length);

            int lineLen = 0;

            //while there are still words left in the queue
            while (wordQueue.TryDequeue(out string res))
            {
                string word = res;
                int counter = 0;

                while (word.Length > maxCharPerLine)
                {
                    //if there's a text already existing in the line, go to the next line
                    if (output.ToString().Length > 0 && counter == 0)
                    {
                        output.Append("\n");
                    }
                    
                    output.Append(word.Substring(0, maxCharPerLine) + "\n");
                    counter++;

                    word = word.Substring(maxCharPerLine);

                    lineLen = 0;
                }

                if (lineLen + word.Length > maxCharPerLine)
                {
                    output.Append("\n");
                    lineLen = 0;
                }

                output.Append(word + " ");

                //add +1 to account for the space between words
                lineLen += word.Length + 1;
            }

            return output.ToString();
        }
    }
}
