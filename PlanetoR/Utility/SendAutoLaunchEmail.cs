using System.Net;
using System.Net.Mail;
using System.Text.Json;
using PlanetoR.Data;
using PlanetoR.Models;
using Quartz;

namespace PlanetoR.Utility;

public class SendAutoLaunchEmail : IJob
{
    private readonly IHttpClientFactory? _httpClientFactory;
    private readonly AppDbContext _dbContext;

    public SendAutoLaunchEmail(IHttpClientFactory? httpClientFactory, AppDbContext dbContext)
    {
        _httpClientFactory = httpClientFactory;
        _dbContext = dbContext;
    }

    public Task Execute(IJobExecutionContext context)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var launchJsonFromApi = httpClient.GetStringAsync("https://fdo.rocketlaunch.live/json/launches/next/1").Result;


        var launchDescription = ExtractDataFromJson.extractString(launchJsonFromApi, "launch_description");
        var winOpen = ExtractDataFromJson.extractString(launchJsonFromApi, "win_open");
        var launchDate = DateTime.Parse(winOpen);


        if (!launchDate.Day.Equals(DateTime.Now.Day)) return Task.FromResult(true);
        var foundUsers = _dbContext.Users.ToList();

        foreach (var user in foundUsers)
        {
            Console.WriteLine(MailSender(user.Email, launchDescription, "Get ready for the upcoming rocket launch! :)"));
        }

        return Task.FromResult(true);
    }

    private string MailSender(string receiver, string messageContent, string subject)
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
            return "email sent to " + receiver;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return "email not sent";
        }
    }
}