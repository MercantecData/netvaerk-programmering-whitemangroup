using System;
using System.Text;

namespace Projekt
{
    class Program
    {
        static void Main(string[] args)
        {
            // OPGAVE 1
            string text1 = "Hej alle!!";
            string text2 = "Øøøh hvad?";
            byte[] custom1_5 = { 72, 101, 106, 33, 33 };
            
            var convertToAscii_1 = Encoding.ASCII.GetBytes(text1);
            var convertToAscii_2 = Encoding.ASCII.GetBytes(text2);
            var convertToUTF8_1 = Encoding.UTF8.GetBytes(text1);
            var convertToUTF8_2 = Encoding.UTF8.GetBytes(text2);

            // Output bliver System.Byte[]
            Console.WriteLine(convertToAscii_1); // [72, 101, 106, 32, 97, 108, 108, 101, 33, 33]
            Console.WriteLine(convertToAscii_2); // [63, 63, 63, 104, 32, 104, 118, 97, 100, 63]  63 = ?
            
            Console.WriteLine(convertToUTF8_1); // [72, 101, 106, 32, 97, 108, 108, 101, 33, 33]
            Console.WriteLine(convertToUTF8_2); // [195, 152, 195, 184, 195, 184, 104, 32, 104, 118, 97, 100, 63]


            // OPGAVE 1.4 - Beskriv processen
            // Encoding.ASCII oversætter chars til tal som kan læses universelt så længe det er inden for tabellen.

            // Encoding.UTF8 oversætter chars til tal (danske bogstaver incl.) og bruger 195 som kontrolnummer ->
            // så når den læses og ser 195 ved programmet at en speciel char kommer


            // OPGAVE 1.5
            string convertBack = Encoding.UTF8.GetString(custom1_5);

            Console.WriteLine(convertBack);


            // OPGAVE BONUS
            byte[] bonusBytes = { 68, 65, 66, 68, 65, 66, 100 }; // 100 for default check
            string[] bonusString = new string[bonusBytes.Length];

            for (int i = 0; i < bonusBytes.Length; i++)
            {
                switch (bonusBytes[i])
                {
                    case 65:
                        bonusString[i] = "A";
                        break;
                    case 66:
                        bonusString[i] = "B";
                        break;
                    case 67:
                        bonusString[i] = "C";
                        break;
                    case 68:
                        bonusString[i] = "D";
                        break;
                    case 69:
                        bonusString[i] = "E";
                        break;
                    default:
                        bonusString[i] = "?";
                        break;
                }
            }

            for (int i = 0; i < bonusString.Length; i++)
            {
                Console.Write(bonusString[i]);
            }




            /*

            // Extra til opgave 1.1, 1.2, 1.3
            // Loops skriver værdien som bliver lavet fra Encoding.ASCII og Encoding.UTF8 istedet for output er "System.Byte[]"

            Console.WriteLine("Opgave 1.1");
            for (int i = 0; i < convertToAscii_1.Length; i++)
            {
                Console.WriteLine(convertToAscii_1[i]);
            }
            Console.WriteLine("");

            Console.WriteLine("Opgave 1.2");
            for (int i = 0; i < convertToAscii_2.Length; i++)
            {
                Console.WriteLine(convertToAscii_2[i]);
            }
            Console.WriteLine("");

            Console.WriteLine("Opgave 1.3");
            for (int i = 0; i < convertToUTF8_1.Length; i++)
            {
                Console.WriteLine(convertToUTF8_1[i]);
            }
            Console.WriteLine("");

            for (int i = 0; i < convertToUTF8_2.Length; i++)
            {
                Console.WriteLine(convertToUTF8_2[i]);
            }

            */
        }
    }
}
