using Task1.DataHandlers.Readers;
using Task1.DataHandlers.Writers;

namespace Task1.MainClasses;

public class Factory
{
    private static Logger? _logger;
    
    private CancellationTokenSource _cts = new ();
    private readonly (string From, string To) _paths;
    private readonly WriteData? _dataWriter;
    private readonly bool _isLoggerEnabled;
    
    private readonly Dictionary<string, FileReader> _dataHandlerStrategies = new()
    {
        { "txt", new FileReader() },
        { "csv", new CsvFileReader() }
    };

    public Factory((string, string) paths, WriteData? dataWriter, bool isLoggerEnabled)
    {
        _paths = paths;
        _dataWriter = dataWriter;
        _isLoggerEnabled = isLoggerEnabled;
    }

    private FileReader? FactoryMethod(string type)
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
        
        var fsw = new FileSystemWatcher(_paths.From);
        
        fsw.EnableRaisingEvents = true;
        
        fsw.NotifyFilter = NotifyFilters.FileName;
        fsw.Filter = "*." + type;
        
        if (_logger is null && _isLoggerEnabled)
            _logger = new Logger(_paths.To);
        
        fsw.Created += async (_, args) =>
        {
            Console.WriteLine(args.FullPath + "args");
            await FileHandler.Handle(args.FullPath, _cts.Token, dataReader, _dataWriter, _logger);
        };
    }

    public void Stop() => _cts.Cancel();
    public void Reset() => _cts = new CancellationTokenSource();
}