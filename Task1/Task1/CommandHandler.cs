namespace Task1;

public static class CommandHandler
{
    private static Factory _factory = null!;
    
    public static void Start(string configPath)
    {
        var paths = ConfigReader.GetPaths(configPath);
        
        if(paths == null)
            Exit();
        
        //@"S:\data"
        
        _factory = new Factory(paths!.Value);
        _factory.Start("txt");
        _factory.Start("csv");
        Activate();
    }
    
    private static void Activate()
    {
        Console.WriteLine("App is working\nEnter 's' for Stop or 'e' for Exit");
        while (true)
        {
            switch (Console.ReadLine()?.ToLower())
            {
                case "s":
                    Stop();
                    break;
                case "e":
                    Exit();
                    break;
            }
        }
    }
    
    private static void Stop()
    {
        Console.Clear();
        _factory.Stop();
        Console.WriteLine("App is stopped\nEnter 'r' for Reset or 'e' for Exit");
        while (true)
        {
            switch (Console.ReadLine()?.ToLower())
            {
                case "r":
                    Reset();
                    break;
                case "e":
                    Exit();
                    break;
            }
        }
    }

    private static void Reset()
    {
        Console.Clear();
        _factory.Reset();
        Activate();
    }
    
    private static void Exit()
    {
        Console.Clear();
        Console.WriteLine("The end");
        Environment.Exit(0);
    }
}