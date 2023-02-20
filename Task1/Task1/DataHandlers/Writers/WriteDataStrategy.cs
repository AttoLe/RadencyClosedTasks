using System.Text.Json;
using Task1.Structures;

namespace Task1.DataHandlers.Writers;

public class JsonWriteData : IWriteData
{
    private static int _counter;
    public void WriteLines(List<DataLine> data, string pathTo)
    {
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
        });
       
        var fullPathTo = pathTo + DateTime.Today.ToString("mm-dd-yyyy");
        if (!Directory.Exists(fullPathTo))
            Directory.CreateDirectory(fullPathTo);
        
        Interlocked.Increment(ref _counter); //todo check why like that (check time on serialize and write apparentrly)
        File.WriteAllText($@"{fullPathTo}/output{_counter}.json", JsonSerializer.Serialize(result, new JsonSerializerOptions{ WriteIndented = true }));
    }
}