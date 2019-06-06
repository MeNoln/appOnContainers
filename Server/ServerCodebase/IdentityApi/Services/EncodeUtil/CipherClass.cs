namespace IdentityApi.Services.EncodeUtil
{
    public static class CipherClass
    {
        private static char cipher(char ch, int key = 5)
        {
            if (!char.IsLetter(ch))
                return ch;

            char d = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - d) % 26) + d);
        }

        //Encode with default key = 5
        public static string Encipher(string input, int key = 5)
        {
            string output = string.Empty;

            foreach (char ch in input)
                output += cipher(ch, key);

            return output;
        }

        //Decode with default key = 5
        public static string Decipher(string input, int key = 5) => Encipher(input, 26 - key);
    }
}
