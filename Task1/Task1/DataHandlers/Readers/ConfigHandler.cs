using Task1.Structures;

namespace Task1.DataHandlers.Readers;

public static class ConfigHandler
{
    public static (string, string)? GetPaths(string configPath)
    {
        if(!configPath.Contains("config.txt"))
            configPath += @"\config.txt";
        
        if (!File.Exists(configPath) || new FileInfo(configPath).Length == 0)
            return null;

        var result = File.ReadAllLines(configPath).Select(x => x.Trim('"')).ToList();
        if (result.Count != 2 || !Directory.Exists(result[0]) || !Directory.Exists(result[1]))
            return null;
        
        return (result[0], result[1]);
    }
}