using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace OpenTraumaRegistry.Shared
{
    public class PasswordHelper
    {
        public string Hash(string Password)
        {
            Random random = new Random();
            var salt = new byte[24];
            var iterations = random.Next(1000, 2000);
            new RNGCryptoServiceProvider().GetBytes(salt); 
            var pbkdf2 = new Rfc2898DeriveBytes(Password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(24); 
            return Convert.ToBase64String(salt) + "|" + iterations + "|" +
                Convert.ToBase64String(hash);
        }

        public bool AuthenticatePassword(string Password, string HashedPassword)
        { 
            var origHashedParts = HashedPassword.Split('|');
            var origSalt = Convert.FromBase64String(origHashedParts[0]);
            var origIterations = Int32.Parse(origHashedParts[1]);
            var origHash = origHashedParts[2]; 
            var pbkdf2 = new Rfc2898DeriveBytes(Password, origSalt, origIterations);
            byte[] testHash = pbkdf2.GetBytes(24);
            var pwd = Convert.ToBase64String(testHash);
            if (pwd == origHash)
                return true;

            //no match return false
            return false; 
        }

        public bool ValididatePasswordFormat(string Password)
        {
            string PasswordPattern = "(?=^[^\\s]{6,}$)(?=.*\\d)(?=.*[a-zA-Z])";
            if (Password != null) return Regex.IsMatch(Password, PasswordPattern);
            else return false;
        }

        public string GetPasswordHidden()
        {
            string pass = "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);
            return pass;
        }

        public bool ValidEmailFormat(string EmailAddress)
        {
            string MatchEmailPattern =
                        @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                 + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				            [0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                 + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				            [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                 + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";

            if (EmailAddress != null) return Regex.IsMatch(EmailAddress, MatchEmailPattern);
            else return false; 
        }

    }
}
