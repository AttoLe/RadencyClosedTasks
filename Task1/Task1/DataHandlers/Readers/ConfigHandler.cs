using Task1.Structures;

namespace Task1.DataHandlers.Readers;

public static class ConfigHandler
{
    public static PathStruct? GetPaths(string configPath)
    {
        if(!configPath.Contains("config.txt"))
            configPath += @"\config.txt";
        
        if (!File.Exists(configPath) || new FileInfo(configPath).Length == 0)
            return null;
        
        return new PathStruct(File.ReadAllLines(configPath).Select(x => x.Trim('"')).ToArray());
    }
}