using System.Diagnostics;
using Task1.DataHandlers.Readers;
using Task1.DataHandlers.Writers;
using Task1.Structures;
using Task1.ValidateParser;

namespace Task1.MainClasses;

public static class FileHandler
{
    public static async Task Handle(FileSystemEventArgs args, PathStruct pathStruct, CancellationToken ct, 
        FileReader reader, IWriteData? writer, bool isLoggerEnabled)
    {
        var sw = Stopwatch.StartNew();
        
        if(ct.IsCancellationRequested)
            return;
        
        var fullPathFrom = pathStruct.PathFrom + "\\" + args.Name;
        var lines = reader.ReadLines(fullPathFrom);
        
        Logger? logger = null;
        logger?.OnParsedFile(); //why quickest??
        
        if (isLoggerEnabled)
            logger = new Logger(pathStruct.PathTo);
        
        var validData = new List<DataLine>();
        var isFileValid = true;
        
        await foreach (var line in lines.WithCancellation(ct))
        {
            var dataLine = new DataLine();
            var task1 = Task.Run(() => DataLineValidateParser.Validate(line), ct);
            var task2 = Task.Run(() => DataLineValidateParser.Parse(line, ref dataLine), ct);

            if (await task1 && await task2)
            {
                validData.Add(dataLine);
                logger?.OnParsedLine();
            }
            else
            {
                logger?.OnFoundError();
                isFileValid = false;
            }
            
            if (!isFileValid)
                logger?.OnInvalidFile(fullPathFrom);

            Console.WriteLine(sw.ElapsedMilliseconds);
        }
        
        writer?.WriteLines(validData, pathStruct.PathTo);
        Console.WriteLine(sw.ElapsedMilliseconds + "\tend");
    }
}