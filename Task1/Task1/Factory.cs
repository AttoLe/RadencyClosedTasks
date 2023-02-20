namespace Task1;

public class Factory
{
    private CancellationTokenSource _cts = new ();
    private readonly PathStruct _paths;
    private static readonly Dictionary<string, Strategy> _strategies = new()
    {
        { "txt", new TxtStrategy() },
        { "csv", new CsvStrategy() }
    };

    public Factory(PathStruct paths) => 
        _paths = paths;
    
    private static Strategy FactoryMethod(string type)
    {
        try
        {
            return _strategies[type];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    } 

    public void Start(string type)
    {
        var strategy = FactoryMethod(type);

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
        fsw.Filter = "*." + type;

        fsw.Created += async (_, args) => await strategy.DoTask(args, _paths, _cts.Token);
    }

    public void Stop() => _cts.Cancel();
    public void Reset() => _cts = new CancellationTokenSource();
}