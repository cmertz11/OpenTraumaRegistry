using System;
using System.Security.Cryptography; 
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace OpenTraumaRegistry.Shared
{
    public class SecurityHelper : ISecurity
    {
        private IConfiguration configuration { get; }
        private string keyString { get; set; }

        private double confirmationTokenExpiresMinutes { get; set; }
        private double tempPasswordExpiresDays { get; set; }

        private double passwordExpiresDays { get; set; }
        public SecurityHelper(IConfiguration _configuration)
        {
            configuration = _configuration;
            keyString = configuration.GetSection("SecuritySettings")["AESKEY"];
            confirmationTokenExpiresMinutes = Convert.ToDouble(configuration.GetSection("SecuritySettings")["CONFIRMATIONTOKENEXPIRESMINUTES"]);
            tempPasswordExpiresDays = Convert.ToDouble(configuration.GetSection("SecuritySettings")["TEMPPASSWORDEXPIRESDAYS"]);
            passwordExpiresDays = Convert.ToDouble(configuration.GetSection("SecuritySettings")["PASSWORDEXPIRESDAYS"]);
        }

        public double TempPasswordExpiresDays()
        {
            return tempPasswordExpiresDays;
        }

        public double ConfirmationTokenExpiresMinutes()
        {
            return confirmationTokenExpiresMinutes;
        }

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

        public string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
            };
            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }

        public string GenerateConfirmationToken()
        {
            return GenerateRandomPassword(new PasswordOptions { RequiredLength = 50, RequireDigit = true, RequiredUniqueChars = 1, RequireLowercase = true, RequireNonAlphanumeric = true, RequireUppercase = true });
        }

        public string EncryptString(string text)
        { 
            byte[] clearBytes = Encoding.Unicode.GetBytes(text);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(keyString, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    text = Convert.ToBase64String(ms.ToArray());
                }
            }
            text = text.Replace("+", "PLUS");
            text = text.Replace("/", "SLASH");
            return text;
        }

        public string DecryptString(string cipherText)
        { 
            cipherText = cipherText.Replace(" ", "+");
            cipherText = cipherText.Replace("PLUS", "+");
            cipherText = cipherText.Replace("SLASH", "/");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(keyString, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
           
            return cipherText;
        }

        public string EncryptTokenObject(string email, string token)
        {
            try
            {
                return EncryptString(email + "|" + token);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public TokenObject DecryptTokenObject(string tokenString)
        {
            try
            {
                var decryptedToken = DecryptString(tokenString);
                return new TokenObject { EmailAddress = decryptedToken.Split("|")[0], Token = decryptedToken.Split("|")[1] };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public double PasswordExpiresDays()
        {
            return passwordExpiresDays;
        }
    }
}
