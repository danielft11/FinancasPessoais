using FinancasPessoais.Domain.Entities;
using System.Net.Mail;
using System.Net;
using FinancasPessoais.Application.Interfaces;
using System;
using System.Net.Http;

namespace FinancasPessoais.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly string HOST = "smtp.gmail.com";
        private readonly string EMAIL = "danielteixeira1985@gmail.com";
        private readonly string GMAIL_APP_PASSWORD = "gasy ccer sflt ldlq";
        
        public void SendAccountPayableEmail(AccountPayable accountPayable) 
        {
            try
            {
                var subject = $"Lembrete de Conta a Pagar - {accountPayable.Description} - Valor: {string.Format("R$ {0:N2}", accountPayable.Value)}";
                string emailBody = $@"
                <html>
                    <body>
                        <p>Olá, a conta <strong>{accountPayable.Description}</strong> no valor de <strong>{string.Format("R$ {0:N2}", accountPayable.Value)}</strong> irá vencer no dia 
                        <strong>{accountPayable.DueDate.ToShortDateString()}</strong>.</p>
                        <p>Utilize o código de barras abaixo para pagar esta conta com o aplicativo de sua preferência.</p>
                         <p>Código de barras: <strong>{accountPayable.BarCode}</strong></p>
                    </body>
                </html>";

                SendEmail(subject, emailBody, accountPayable.Emails);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not send e-mail. Exception caught: " + ex);
            }
            
        }

        public void SendResetPasswordEmail(string email, string callbackUrl) 
        {
            try
            {
                var subject = $"Recuperação de senha";
                string emailBody = $@"
                <html>
                    <body>
                        <p>Olá,</p>
                        <p>Recebemos uma solicitação para altera sua senha em nosso sistema, e por isto você está recebendo este e-mail.</p>
                        <p>Para alterar sua senha clique no link abaixo e crie uma nova senha:</p>
                        <p><a href='{callbackUrl}'>Alterar senha</a></p>
                        <p>Caso você não tenha solicitado esta recuperação de senha, por favor, desconsidere este e-mail.</p> 
                        <p>Atenciosamente,</p>
                        <p>Equipe de Suporte - MyFinance</p>    
                    </body>
                </html>";

                SendEmail(subject, emailBody, email);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not send e-mail. Exception caught: " + ex);
            }
        }

        private void SendEmail(string subject, string body, string recipient) 
        {
            var smtpClient = CreateSMTPClient();

            var mailMessage = new MailMessage
            {
                From = new MailAddress(EMAIL),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(recipient);

            smtpClient.Send(mailMessage);
        }

        private SmtpClient CreateSMTPClient()
        {
            return new SmtpClient
            {
                Host = HOST,
                Port = 587,
                Credentials = new NetworkCredential(EMAIL, GMAIL_APP_PASSWORD),
                EnableSsl = true,
            };
        }

    }
}
