namespace Task1.DataHandlers.Readers;

public class FileReader
{
    public virtual async IAsyncEnumerable<string> ReadLines(string fullPath)
    {
        var streamReader = new StreamReader(fullPath);
        while (!streamReader.EndOfStream)
        {
            var line = await streamReader.ReadLineAsync();
            if(string.IsNullOrWhiteSpace(line))
                continue;
            
            yield return line;
        }
    }
}