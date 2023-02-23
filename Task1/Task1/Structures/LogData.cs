using System.Collections.Concurrent;

namespace Task1.Structures;

public struct LogData
{
    public int ParsedFiles, ParsedLines, FoundErrors;
    public ConcurrentDictionary<string, bool> InvalidFiles;

    public LogData()
    {
        FoundErrors = 0;
        ParsedFiles = 0;
        ParsedLines = 0;
        InvalidFiles = new ConcurrentDictionary<string, bool>();
    }
}