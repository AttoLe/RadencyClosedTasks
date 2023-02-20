
namespace Task1;

public struct DataLine
{
    public string Name = string.Empty, City = string.Empty, Service = string.Empty;
    public decimal Payment = 0;
    public DateOnly Date = default;
    public long AccountNumber = 0;

    public DataLine() { }
}

public struct PathStruct
{
    public readonly string PathFrom, PathTo;

    public PathStruct(string[] paths) => 
        (PathFrom, PathTo) = (paths[0], paths[1]);
    
    
    public PathStruct(string pathTo, string pathFrom)
    {
        PathFrom = pathFrom;
        PathTo = pathTo;
    }
}

/*public sealed class DataLineMap : ClassMap<DataLine>
{
    public DataLineMap()
    {
        try
        {
            Map(m => m.FirstName).Index(0);
            Map(m => m.LastName).Index(1);
            Map(m => m.City).Index(2);
            Map(m => m.Payment).Index(5);
            Map(m => m.Date).Index(6).TypeConverter<DateOnlyConverter>();
            Map(m => m.AccountNumber).Index(7);
            Map(m => m.Service).Index(8);
        }
        catch (Exception)
        {
            // ignored
        }
    }
    //x => DateOnly.ParseExact(x, "yyyy-dd-mm", CultureInfo.InvariantCulture)
}

public class DateOnlyConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        //var v = base.ConvertFromString(text, row, memberMapData)?.ToString();
        return DateOnly.ParseExact(text!.Trim(), "yyyy-dd-mm", CultureInfo.InvariantCulture);
    }
}*/