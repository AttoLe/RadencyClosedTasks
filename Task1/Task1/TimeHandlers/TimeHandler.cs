using Task1.DataHandlers.Writers;

namespace Task1.TimeHandlers;

public class TimeHandler
{
    private Logger? _logger;
    
    public void ActivateTimer(Logger logger, int hour, int periodHour)
    {
        _logger = logger;
        var timer = new Timer(Callback);
        var endTime = DateTime.Today.AddHours(hour);
        
        timer.Change(endTime - DateTime.Now, new TimeSpan(periodHour, 0, 0));
    }

    private void Callback(object? state) => _logger?.WriteLog();
}