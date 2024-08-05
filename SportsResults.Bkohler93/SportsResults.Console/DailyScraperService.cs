using Microsoft.Extensions.Hosting;

namespace SportsResults;

public class DailyScraperService : BackgroundService
{
    private readonly TimeSpan _timeToRun = new TimeSpan(11, 22, 0);
    private Timer? _timer;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        TimeSpan currentTime = DateTime.Now.TimeOfDay;
        TimeSpan initialDelay = _timeToRun > currentTime 
            ? _timeToRun - currentTime 
            : new TimeSpan(24, 0, 0) - (currentTime - _timeToRun);

        _timer = new Timer(PerformDailyTask, null, initialDelay, new TimeSpan(24, 0, 0));

        return Task.CompletedTask;
    }

    private void PerformDailyTask(object? state)
    {
        var emailBody = SportsScraper.Scrape();
        EmailService.SendEmail(emailBody);
    }

    public override void Dispose()
    {
        _timer?.Dispose();
        base.Dispose();
    }
}