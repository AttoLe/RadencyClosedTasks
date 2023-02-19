using System.Diagnostics;

namespace Task1;


public interface IStrategy
{
    public Task DoTask(FileSystemEventArgs args, string pathFrom, string pathTo, CancellationToken ct);
}

public class TxtStrategy : IStrategy
{
    public async Task DoTask(FileSystemEventArgs args, string pathFrom, string pathTo, CancellationToken ct)
    {
        var sw = Stopwatch.StartNew();
        if(ct.IsCancellationRequested)
            return;
        
        var lines = ReadLines(pathFrom + "\\" + args.Name);

        await foreach (var line in lines.WithCancellation(ct))
        {
            var dataLine = new DataLine();
            var task1 = Task.Run(() => DataLineValidateParserTxt.Validate(line), ct);
            var task2 = Task.Run(() => DataLineValidateParserTxt.ParserTxt(line, ref dataLine), ct);
            if (await task1 && await task2)
            {
                Console.WriteLine(dataLine.FirstName + "\t" + dataLine.LastName);
                //TODO write dataLine to JSON
            }
            else
            {
                //TODO write to log
            }

            Console.WriteLine(sw.ElapsedMilliseconds);
        }
    }

    private async IAsyncEnumerable<string> ReadLines(string fullPath)
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