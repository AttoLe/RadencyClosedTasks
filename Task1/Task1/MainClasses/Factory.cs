using Task1.DataHandlers.Readers;
using Task1.DataHandlers.Writers;
using Task1.Structures;

namespace Task1.MainClasses;

public class Factory
{
    private CancellationTokenSource _cts = new ();
    private readonly PathStruct _paths;
    
    private readonly IWriteData? _dataWriter;
    private readonly bool _isLoggerEnabled;
    
    private static readonly Dictionary<string, FileReader> _dataHandlerStrategies = new()
    {
        { "txt", new FileReader() },
        { "csv", new CsvFileReader() }
    };

    public Factory(PathStruct paths, IWriteData? dataWriter, bool isLoggerEnabled)
    {
        _paths = paths;
        _dataWriter = dataWriter;
        _isLoggerEnabled = isLoggerEnabled;
    }

    private static FileReader? FactoryMethod(string type)
    {
        try
        {
            return _dataHandlerStrategies[type];
        }
        catch (Exception)
        {
            return null;
        }
    } 

    public void Start(string type)
    {
        var dataReader = FactoryMethod(type);
        if(dataReader is null)
            return;
        
        var fsw = new FileSystemWatcher(_paths.PathFrom);
        
        fsw.EnableRaisingEvents = true;
        
        fsw.NotifyFilter = NotifyFilters.Attributes
                           | NotifyFilters.CreationTime
                           | NotifyFilters.DirectoryName
                           | NotifyFilters.FileName
                           | NotifyFilters.LastAccess
                           | NotifyFilters.LastWrite
                           | NotifyFilters.Security
                           | NotifyFilters.Size;
        //todo delete extra filters
        fsw.Filter = "*." + type;
        
        fsw.Created += async (_, args) => await FileHandler.Handle(args, _paths, _cts.Token, dataReader, _dataWriter, _isLoggerEnabled);
    }

    public void Stop() => _cts.Cancel();
    public void Reset() => _cts = new CancellationTokenSource();
}