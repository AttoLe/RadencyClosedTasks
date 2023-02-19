namespace Task1;

public struct DataLine
{
    public string FirstName = string.Empty, LastName = string.Empty, City = string.Empty, Service = string.Empty;
    public decimal Payment = 0;
    public DateOnly Date = default;
    public long AccountNumber = 0;

    public DataLine() { }
}