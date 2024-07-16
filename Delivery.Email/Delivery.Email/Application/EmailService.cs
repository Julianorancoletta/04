using Delivery.Email.Core.Configuration;
using Delivery.Email.Core.Domain;
using Delivery.Email.Worker.Application.Interfaces;
using Delivery.Email.Worker.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;

public class EmailService : IEmailService
{
    private EmailConfiguration _emailConfiguration;
    public EmailService(IOptions<EmailConfiguration> options)
    {
        _emailConfiguration = options.Value;
    }

    public void envio(PessoaFisica pessoa)
    {
        // Configurações do servidor SMTP
        string smtpAddress = _emailConfiguration.remetenteEmail;
        int portNumber = 587;
        bool enableSSL = true;

        // Credenciais de login
        string emailFromAddress = _emailConfiguration.remetenteEmail; // Remetente
        string password = _emailConfiguration.remetenteSenha; // Senha do remetente
        string emailToAddress = pessoa.email; // Destinatário
        string subject = "Cadatro";
        string body = $"Cadastro realizado com sucesso{pessoa.nomeCompleto}";

        using (MailMessage mail = new MailMessage())
        {
            mail.From = new MailAddress(emailFromAddress);
            mail.To.Add(emailToAddress);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            // Se você quiser adicionar um anexo
            // mail.Attachments.Add(new Attachment("caminho/para/o/anexo.txt"));

            using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
            {
                smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                smtp.EnableSsl = enableSSL;
                try
                {
                    smtp.Send(mail);
                    Console.WriteLine("E-mail enviado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao enviar o e-mail: " + ex.Message);
                }
            }
        }
    }
}
