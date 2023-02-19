using System.Globalization;
using System.Text.RegularExpressions;

namespace Task1;

public static class DataLineValidateParser
{
    public static bool Validate(string line)
    {
        var allRegex =
            @"^([A-Z]\w+(\,\s)){2}(“.+”(\,\s))(\d+(\.\d+)?(\,\s))(\d{4}(\-\d{2}){2}(\,\s))(\d+)(\,\s)([A-Z]\w+)$";
        
        return Regex.IsMatch(line, allRegex);
    }

    public static bool Parser(string line, ref DataLine dataLine)
    {
        try
        {
            var i1 = line.IndexOf('“', StringComparison.InvariantCulture);
            var i2 = line.IndexOf("”,", StringComparison.InvariantCulture);

            var fullAddress = line.Substring(i1, i2 - i1 + 1);
            dataLine.City = fullAddress.Split(',').First().Trim();
            
            var data = line.Replace(fullAddress, "").Split(", ")
                .Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            
            dataLine.FirstName = data[0];
            dataLine.LastName = data[1];
            dataLine.Payment = Convert.ToDecimal(data[2]);
            dataLine.Date = DateOnly.ParseExact(data[3], "yyyy-dd-mm", CultureInfo.InvariantCulture);
            dataLine.AccountNumber = Convert.ToInt64(data[4]);
            dataLine.Service = data[5];
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}