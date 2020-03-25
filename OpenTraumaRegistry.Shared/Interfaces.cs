using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenTraumaRegistry.Shared
{
    public interface IEmailHelper
    {
        Task SendAsync(EmailObj email);
        string GenerateEmailConfirmationRequest(string firstName, string tempPassword, string confirmationToken);
 
    }

    public interface ISecurity
    { 
        double TempPasswordExpiresDays();

        double ConfirmationTokenExpiresMinutes();

        double PasswordExpiresDays();

        string Hash(string Password);
        bool AuthenticatePassword(string Password, string HashedPassword);
        bool ValididatePasswordFormat(string Password);

        string GetPasswordHidden();

        bool ValidEmailFormat(string EmailAddress);

        string GenerateRandomPassword(PasswordOptions opts = null);

        string GenerateConfirmationToken();

        string EncryptString(string text);

        string DecryptString(string cipherText);

        string EncryptTokenObject(string email, string token);

        TokenObject DecryptTokenObject(string tokenString);

    }
 
}
