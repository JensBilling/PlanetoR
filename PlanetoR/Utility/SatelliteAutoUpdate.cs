using Quartz;

namespace PlanetoR.Utility;

public class SatelliteAutoUpdate : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        var x = DateTime.Now;
        Console.WriteLine(x);
        return Task.FromResult(true);
    }


}