namespace Task1.DataHandlers.Readers;

public class FileReader
{
    public virtual async IAsyncEnumerable<string> ReadLines(string fullPath)
    {
        //Console.WriteLine(fullPath);vb 
        await using var fs = new FileStream(fullPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
        using var streamReader = new StreamReader(fs);
        while (!streamReader.EndOfStream)
        {
            var line = await streamReader.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(line))
                continue;

            yield return line;
        }
    }
}