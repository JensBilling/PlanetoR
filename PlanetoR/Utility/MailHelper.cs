using System.Net;
using System.Net.Mail;

namespace PlanetoR.Utility;

public static class MailHelper
{
    public static void MailSender(string receiver, string messageContent, string subject)
    {
        var to = new MailAddress(receiver);
        var from = new MailAddress("planetor@jensbilling.se");
        var message = new MailMessage(from, to);
        message.Subject = subject;
        message.Body = messageContent;

        var client = new SmtpClient("smtp.simply.com", 587)
        {
            Credentials = new NetworkCredential("planetor@jensbilling.se", "planetor666"),
            EnableSsl = true
        };

        try
        {
            client.Send(message);
            Console.WriteLine("email sent to " + receiver);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            Console.WriteLine("email not sent");
        }
    }
}