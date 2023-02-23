using System.Text.Json;
using Task1.Structures;

namespace Task1.DataHandlers.Writers;

public class JsonWriteData : WriteData
{
    public JsonWriteData(string pathToFolder) : base(pathToFolder) { }
    
    public override void WriteLines(List<DataLine> data)
    {
        Interlocked.Increment(ref _counter);
        var localCounter = _counter;
        
        var result = data.GroupBy(line => line.City, (city, services) => new DataJson()
        {
            City = city,
            Services = services
                .GroupBy(
                    service => service.Service,
                    (serviceName, payers) => new DataJson.Service()
                    {
                        Name = serviceName,
                        Payers = payers.Select(payer => new DataJson.Payer()
                        {
                            Name = payer.Name,
                            AccountNumber = payer.AccountNumber,
                            Date = payer.Date.ToString(),
                            Payment = payer.Payment,
                        }).ToList(),
                        Total = payers.Sum(x => x.Payment)
                    }),
            Total = services.Sum(x => x.Payment)
        }).ToList();

        var fullPath = _PathToFolder + DateTime.Today.ToString("MM-dd-yyyy");
        if (!Directory.Exists(fullPath))
            Directory.CreateDirectory(fullPath);
        
        File.WriteAllText($@"{fullPath}/output{localCounter}.json",
            JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true }));
    }
}