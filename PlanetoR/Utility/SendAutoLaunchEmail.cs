using PlanetoR.Data;

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
            MailHelper.MailSender(user.Email, launchDescription, "Get ready for the upcoming rocket launch! :)");
        }

        return Task.FromResult(true);
    }
    
}