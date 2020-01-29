using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OpenTraumaRegistry.Shared
{
    public class _SendGrid : IEmailHelper
    {
        private IConfiguration configuration { get; }
        private string ApiKey { get; set; }
        private string EmailConfirmationTemplate { get; set; }
        public string TempPasswordTemplate { get; private set; }
        private string HostName { get; set; }

        private string SystemEmailAddress { get; set; }

        public _SendGrid(IConfiguration _configuration)
        {
            configuration = _configuration;
            ApiKey = configuration.GetSection("EmailSettings")["ApiKey"];
            EmailConfirmationTemplate = configuration.GetSection("EmailSettings")["EmailConfirmationTemplate"];
            TempPasswordTemplate = configuration.GetSection("EmailSettings")["EmailConfirmationTemplate"];
            HostName = configuration.GetSection("EmailSettings")["HostName"];
            SystemEmailAddress = configuration.GetSection("EmailSettings")["SystemEmailAddress"];
        }
        public async Task SendAsync(EmailObj email)
        {
            try
            {
                var emailClient = new SendGridClient(ApiKey);
                var from = new EmailAddress(SystemEmailAddress, "Open Trauma Registry");
                
                var to = new EmailAddress(email.to, email.to);
               
                var msg = MailHelper.CreateSingleEmail(from, to, email.subject, "", email.htmlContent);
                var response = await emailClient.SendEmailAsync(msg);

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public string GenerateEmailConfirmationRequest(string firstName, string tempPassword, string confirmationToken)
        {
            string url = HostName + "/login/" + confirmationToken;
            string path = Directory.GetCurrentDirectory();
            string htmlContent = File.ReadAllText(path + EmailConfirmationTemplate); 
            htmlContent = htmlContent.Replace("[NAME]", firstName);
            htmlContent = htmlContent.Replace("[TEMP_PASSWORD]", tempPassword);
            htmlContent = htmlContent.Replace("[CONFIRMATION_URL]", url);
            htmlContent = htmlContent.Replace("[FROM_YOUR_TEAM]", "Open Trauma Registry Team");
            return htmlContent; 
        }
    }
}
