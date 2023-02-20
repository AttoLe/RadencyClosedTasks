using Task1.DataHandlers.Readers;
using Task1.DataHandlers.Writers;
using Task1.MainClasses;

namespace Task1.ControlClasses;

public static class CommandHandler
{
    private static Factory _factory = null!;
    
    public static void Start(string configPath)
    {
        var paths = ConfigHandler.GetPaths(configPath);
        
        if(paths == null)
            Exit();

        _factory = new Factory(paths!.Value, new JsonWriteData(), true);
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