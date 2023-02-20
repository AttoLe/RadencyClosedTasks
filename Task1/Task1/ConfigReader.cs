namespace Task1;

public static class ConfigReader
{
    public static PathStruct? GetPaths(string configPath)
    {
        if (!File.Exists(configPath + "config.txt") || new FileInfo(configPath).Length == 0)
            return null;
        
        return new PathStruct(File.ReadAllLines(configPath));
    }
}