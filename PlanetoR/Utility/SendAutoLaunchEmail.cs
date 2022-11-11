using System.Net;
using System.Net.Mail;
using System.Text.Json;
using PlanetoR.Models;
using Quartz;

namespace PlanetoR.Utility;

public class SendAutoLaunchEmail : IJob
{
    private readonly IHttpClientFactory? _httpClientFactory;

    public SendAutoLaunchEmail(IHttpClientFactory? httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public Task Execute(IJobExecutionContext context)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var launchJsonFromApi = httpClient.GetStringAsync("https://fdo.rocketlaunch.live/json/launches/next/1").Result;


        var launchDescription = ExtractDataFromJson.extractString(launchJsonFromApi, "launch_description");
        var winOpen = ExtractDataFromJson.extractString(launchJsonFromApi, "win_open");
        var launchDate = DateTime.Parse(winOpen);
        
        if (launchDate.Day.Equals(DateTime.Now.Day))
        {
            Console.WriteLine(MailSender("jonasgranbom@hotmail.com", launchDescription, "A rocket will launch today!"));
            Console.WriteLine(MailSender("jensis92@hotmail.com", launchDescription, "A rocket will launch today!"));
            Console.WriteLine(MailSender("gunnarssonminna@gmail.com", launchDescription, "A rocket will launch today! VISA LITE ENGAGEMANG FÖR HELVETE, BABY!!!"));
        }

        return Task.FromResult(true);
    }

    private string MailSender(string receiver, string messageContent, string subject)
    {
        MailAddress to = new MailAddress(receiver);
        MailAddress from = new MailAddress("planetor@jensbilling.se");
        MailMessage message = new MailMessage(from, to);
        message.Subject = subject;
        message.Body = messageContent;

        SmtpClient client = new SmtpClient("smtp.simply.com", 587)
        {
            Credentials = new NetworkCredential("planetor@jensbilling.se", "planetor666"),
            EnableSsl = true
        };

        try
        {
            client.Send(message);
            return "email sent to " + receiver;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return "email not sent";
        }


        
    }
}