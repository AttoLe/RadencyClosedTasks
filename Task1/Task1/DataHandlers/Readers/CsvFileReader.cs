namespace Task1.DataHandlers.Readers;

public class CsvFileReader : FileReader
{
    public override async IAsyncEnumerable<string> ReadLines(string fullPath)
    {
        using var streamReader = new StreamReader(fullPath);
        
        await streamReader.ReadLineAsync();
        
        while (!streamReader.EndOfStream)
        {
            var line = await streamReader.ReadLineAsync();
            if(string.IsNullOrWhiteSpace(line))
                continue;
            
            yield return line;
        }
        streamReader.Close();
    }
}