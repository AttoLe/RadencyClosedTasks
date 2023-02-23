namespace Task1.Structures;

public struct DataJson
{
    public string City { get; set; }
    public IEnumerable<Service> Services { get; set; }
    public decimal Total { get; set; }

    public struct Service
    {
        public string Name { get; set; }
        public List<Payer> Payers { get; set; }
        public decimal Total { get; set; }
    }

    public struct Payer
    {
        public string Name { get; set; }
        public decimal Payment { get; set; }
        public string Date { get; set; }
        public long AccountNumber { get; set; }
    }
}