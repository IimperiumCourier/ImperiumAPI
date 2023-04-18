using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.Infrastructure.Models;
using ImperiumLogistics.SharedKernel.Setting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Infrastructure.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _httpClient;
        private readonly EmailSetting _emailSetting;
        public EmailService(IOptions<EmailSetting> emailSetting, HttpClient httpClient)
        {
            _emailSetting = emailSetting.Value;
            _httpClient = httpClient;
           // _httpClient.BaseAddress = new Uri(_emailSetting.BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new
            MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task SendMail(EmailRequestDto requestDto)
        {
            SmtpClient smtp = new SmtpClient();
            MailMessage mail = new MailMessage();
            try
            {

                //set the addresses
                mail.From = new MailAddress("no-reply@iimperiumlogistics.com"); 
                mail.To.Add(requestDto.Recepient);

                //set the content
                mail.Subject = requestDto.Subject;
                mail.Body = requestDto.Body;
                mail.IsBodyHtml = requestDto.IsHtml;

                smtp = new SmtpClient("mail.iimperiumlogistics.com");

                NetworkCredential Credentials = new NetworkCredential("no-reply@iimperiumlogistics.com", "3^10aB2qb");
                smtp.Credentials = Credentials;
                smtp.Port = 587;
                await smtp.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                mail.Dispose();
                smtp.Dispose();
            }
        }
    }
}
