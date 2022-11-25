using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;

namespace PassGen;

class Program
{
    
    static RandomNumberGenerator servProvider = RandomNumberGenerator.Create();

    static void Main()
    {
        int PassQuantity, PassLength = 0;
        
        string UpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string LowerCase = "abcdefghijklmnopqrstuvwxyz";
        string AlphaNumeric = "0123456789";
        string SpecialCharacters = "!@#$%^&*()-_=+<,>.";



        Console.WriteLine("\nHow many Passes do you want?");
        PassQuantity = int.Parse(Console.ReadLine());
        Console.WriteLine("What is the lenght of the Passes?");
        PassLength = int.Parse(Console.ReadLine());

        string[] AllPasses = new string[PassQuantity];


        string FinalString = UpperCase + LowerCase + AlphaNumeric + SpecialCharacters;
        for (int i = 0; i < PassQuantity; i++)
        {
            StringBuilder strBuilder = new StringBuilder();
            for (int n = 0; n < PassLength; n++)
            {
                strBuilder = strBuilder.Append(GenerateChar(FinalString));
            }

            AllPasses[i] = strBuilder.ToString();
        }

        Console.WriteLine("Generated passwords:");

        int PassCount = 0;
        foreach (string Pass in AllPasses)
        {
            PassCount++;
            byte[] strPass = Encoding.UTF8.GetBytes(Pass);
            string toB64 = System.Convert.ToBase64String(strPass);

            Console.WriteLine(PassCount + " Pass: " + Pass + "\r\n");
            Console.WriteLine(PassCount + " Base64Pass: " + toB64 + "\r\n\r\n");

        }


    }

    private static char GenerateChar(string availableChars)
    {
        var byteArr = new byte[1];
        char ch;
        do
        {
            servProvider.GetBytes(byteArr);
            ch = (char)byteArr[0];

        } while (!availableChars.Any(x => x == ch));

        return ch;
    }
}
