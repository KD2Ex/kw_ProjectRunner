using System.Collections.Generic;

namespace _6_Scripts.Utils.DataFormating
{
    public class DataFormating
    {
        public static string FormatIntData(int value)
        {
            string result = "";
            List<int> extractedNumbers = new();
            var count = value;

        
            if (count == 0)
            {
                return "<sprite=0>";
            }
        
            while (count > 0)
            {
                var number = count % 10;
                extractedNumbers.Add(number);
                count /= 10;
            }

            extractedNumbers.Reverse();
            foreach (var number in extractedNumbers)
            {
                result += $"<sprite={number}>";
            }

            return result;
        }
        
        public static  string FormatIntData(int value, out int digitsNumber)
        {
            string result = "";
            List<int> extractedNumbers = new();
            var count = value;

        
            if (count == 0)
            {
                digitsNumber = 0;
                return "<sprite=0>";
            }
        
            while (count > 0)
            {
                var number = count % 10;
                extractedNumbers.Add(number);
                count /= 10;
            }

            extractedNumbers.Reverse();
            foreach (var number in extractedNumbers)
            {
                result += $"<sprite={number}>";
            }

            digitsNumber = extractedNumbers.Count;
            return result;
        }
    }
}