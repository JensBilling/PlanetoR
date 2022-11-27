using System.Net;
using System.Net.Mail;

namespace PlanetoR.Utility;

public static class MailHelper
{
    public static void MailSender(string receiver, string messageContent, string subject)
    {

        Console.WriteLine("We were supposed to send an email to: " + receiver + " with subject: '" + subject + "'. But PlanetoR's email address has been temporarily blocked for spam.");
        
        // Commented code below is a quick disable of mail-sender because email address has been tagged for spam
        
        /*var to = new MailAddress(receiver);
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
        }*/
    }
}