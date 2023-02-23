namespace Task1.DataHandlers.Readers;

public class CsvFileReader : FileReader
{
    public override async IAsyncEnumerable<string> ReadLines(string fullPath)
    {
        Console.WriteLine(fullPath + "0");
        using var streamReader = new StreamReader(fullPath);
        Console.WriteLine(fullPath + "1");
        await streamReader.ReadLineAsync();
        Console.WriteLine(fullPath + "2");
        while (!streamReader.EndOfStream)
        {
            Console.WriteLine(fullPath + "3");
            var line = await streamReader.ReadLineAsync();
            if(string.IsNullOrWhiteSpace(line))
                continue;
            
            yield return line;
        }
        streamReader.Close();
    }
}