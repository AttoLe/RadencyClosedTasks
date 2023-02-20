using System.Diagnostics;

namespace Task1;

public abstract class Strategy
{
    public async Task DoTask(FileSystemEventArgs args, PathStruct pathStruct, CancellationToken ct)
    {
        var sw = Stopwatch.StartNew();
        if(ct.IsCancellationRequested)
            return;
        
        var lines = ReadLines(pathStruct.PathFrom + "\\" + args.Name);

        await foreach (var line in lines.WithCancellation(ct))
        {
            var dataLine = new DataLine();
            var task1 = Task.Run(() => DataLineValidateParser.Validate(line), ct);
            var task2 = Task.Run(() => DataLineValidateParser.Parser(line, ref dataLine), ct);
            if (await task1 && await task2)
            {
                Console.WriteLine(dataLine.Name);
                //TODO write dataLine to JSON
            }
            else
            {
                //TODO write to log
            }

            Console.WriteLine(sw.ElapsedMilliseconds);
        }
    }

    protected abstract IAsyncEnumerable<string> ReadLines(string fullPath);
}

public class TxtStrategy : Strategy
{
    protected override async IAsyncEnumerable<string> ReadLines(string fullPath)
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

public class CsvStrategy : Strategy
{
    protected override async IAsyncEnumerable<string> ReadLines(string fullPath)
    {
        var streamReader = new StreamReader(fullPath);
        await streamReader.ReadLineAsync();
        
        while (!streamReader.EndOfStream)
        {
            var line = await streamReader.ReadLineAsync();
            if(string.IsNullOrWhiteSpace(line))
                continue;
            
            yield return line;
        }

        /*var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        };
         
        var csvReader = new CsvReader(new StreamReader(fullPath), config);
         
        //var f = new DataLineMap(csvReader.HeaderRecord!);
        csvReader.Context.RegisterClassMap<DataLineMap>();
         
        while (await csvReader.ReadAsync())
        {
             
            var line = csvReader.GetRecord<DataLine>();
            Console.WriteLine(line.FirstName + "\t" + line.LastName);
            //if(string.IsNullOrWhiteSpace(line))
            //continue;
             
            yield return "line";
        }*/
    }
}