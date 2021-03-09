using System;
using System.Diagnostics;
using System.Text;

namespace AuraUtilities
{
    public class RandomGenerator
    {
        public RandomGenerator()
        {
            Debug.WriteLine("random created");
        }

        // Instantiate random number generator.
        // It is better to keep a single Random instance
        // and keep using Next on the same instance.
        private readonly Random _random = new Random();

        // Generates a random number within a range.
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        // Generates a random string with a given size.
        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.

            // char is a single Unicode character
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        // Generates a random password.
        // 4-LowerCase + 4-Digits + 2-UpperCase
        public string RandomPassword()
        {
            var passwordBuilder = new StringBuilder();

            // 4-Letters lower case
            passwordBuilder.Append(RandomString(30, true));

            // 5-Digits between 10000 and 99999
            passwordBuilder.Append(RandomNumber(10000, 99999));

            // 6-Letters upper case
            passwordBuilder.Append(RandomString(6));
            return passwordBuilder.ToString();
        }
    }
}