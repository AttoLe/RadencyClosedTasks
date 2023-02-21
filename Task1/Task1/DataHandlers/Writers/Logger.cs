using Task1.Structures;
using Task1.TimeHandlers;

namespace Task1.DataHandlers.Writers;

public class Logger
{
    private string _pathTo;
    private LogData _logData = new();

    public Logger(string pathTo)
    {
        _pathTo = pathTo;
         new TimeHandler().ActivateTimer(this, 24, 24);
    }

    public void OnParsedFile() => Interlocked.Increment(ref _logData.ParsedFiles);
    public void OnParsedLine() => Interlocked.Increment(ref _logData.ParsedLines);
    public void OnFoundError() => Interlocked.Increment(ref _logData.FoundErrors);
    public void OnInvalidFile(string fullFilePath) => _logData.InvalidFiles.TryAdd(fullFilePath, default);

    public void WriteLog()
    {
        var fullPathTo = _pathTo + DateTime.Today.Subtract(new TimeSpan(0, 5, 0)).ToString("MM-dd-yyyy");
        if(!Directory.Exists(fullPathTo))
            return;
        
        var result = new[]
        {
            "parsed_files: " + _logData.ParsedFiles,
            "parsed_lines: " + _logData.ParsedLines,
            "found_errors: " + _logData.FoundErrors,
            $"invalid_files: [{string.Join(", ", _logData.InvalidFiles.Select(inf => inf.Key))}]"
        };
        File.WriteAllLines(fullPathTo + @"\meta.log", result);
        Console.WriteLine("logged");
    }
}