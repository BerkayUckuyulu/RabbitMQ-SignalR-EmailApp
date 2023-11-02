using System;
using System.Net;
using System.Net.Mail;

namespace EMailSender
{
	public static class EMailSender
	{
		public static void Send(string to,string message)
		{
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp-gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            NetworkCredential credential = new NetworkCredential("berkayProjectTrial@gmail.com", "");
            smtpClient.Credentials = credential;
            MailAddress sender = new MailAddress("berkayProjectTrial@gmail.com", "Message message");
            MailAddress toto = new MailAddress(to);
            MailMessage mail = new MailMessage(sender, toto);
            mail.Subject = "Example";
            mail.Body = message;
            smtpClient.Send(mail);
        }
	}
}

