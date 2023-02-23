using System.Text.Json;

namespace Task1.DataHandlers.Readers;

public static class ConfigHandler
{
    public static (string, string) GetPaths(string configPath)
    {
        if (!configPath.Contains("config.json"))
            configPath += @"\config.json";
        
        if (!File.Exists(configPath))
            throw new Exception("No config");

        var result = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(configPath));
        
        /*var result = File.ReadAllLines(configPath).Select(x => x.Trim('"')).ToList();
        if (result.Count != 2 || !Directory.Exists(result[0]) || !Directory.Exists(result[1]))
            throw new Exception("Config is empty");*/
        
        return (result["from"], result["to"]);
    }
}