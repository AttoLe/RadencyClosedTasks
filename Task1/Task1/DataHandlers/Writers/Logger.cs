using Task1.Structures;

namespace Task1.DataHandlers.Writers;

public class Logger
{
    private string _pathTo;
    private LogData _logData = new();

    public Logger(string pathTo)
    {
        _pathTo = pathTo;
    }

    public void OnParsedFile() => Interlocked.Increment(ref _logData.ParsedFiles);
    public void OnParsedLine() => Interlocked.Increment(ref _logData.ParsedLines);
    public void OnFoundError() => Interlocked.Increment(ref _logData.FoundErrors);
    public void OnInvalidFile(string fullFilePath) => _logData.InvalidFiles.TryAdd(fullFilePath, default);

    public void WriteLog()
    {
       
    }

    public void WriteLines(string fullPathTo)
    {
        //todo write log with date class
        //File.WriteAllLines(@"S:\passwords.log", new[]{ "safasf", "ffaaf" });
    }
}