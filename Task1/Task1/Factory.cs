namespace Task1;

public class Factory
{
    private CancellationTokenSource _cts = new ();
    
    private static readonly Dictionary<string, Strategy> _strategies = new()
    {
        { "txt", new TxtStrategy() },
        { "csv", new CsvStrategy() }
    };
    
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

    public void Start(string pathFrom, string pathTo, string type)
    {
        var strategy = FactoryMethod(type);

        var fsw = new FileSystemWatcher(pathFrom);
        
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

        fsw.Created += async (_, args) => await strategy.DoTask(args, pathFrom, pathTo, _cts.Token);
    }

    public void Stop() => _cts.Cancel();
    public void Reset() => _cts = new CancellationTokenSource();
}