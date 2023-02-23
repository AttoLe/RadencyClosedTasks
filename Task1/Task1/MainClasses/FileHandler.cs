using Task1.DataHandlers.Readers;
using Task1.DataHandlers.Writers;
using Task1.Structures;
using Task1.ValidateParser;

namespace Task1.MainClasses;

public static class FileHandler
{
    public static async Task Handle(string fullPath, CancellationToken ct, 
        FileReader reader, WriteData? writer, Logger? logger)
    {
        if(ct.IsCancellationRequested)
            return;
        
        var lines = reader.ReadLines(fullPath);
        
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
                isFileValid = false;
                logger?.OnFoundError();
            }
            
            if (!isFileValid)
                logger?.OnInvalidFile(fullPath);
        }
        
        logger?.OnParsedFile();
        writer?.WriteLines(validData);
    }
}