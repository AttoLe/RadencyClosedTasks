namespace Task1.Structures;

public struct DataJson
{
    public string City { get; set; }
    public IEnumerable<Service> Services { get; set; }
    public decimal Total { get; set; }

    public DataJson(string city, IEnumerable<Service> services)
    {
        City = city;
        Services = services;
        Total = Services.Aggregate(0m, (temp, res) => Convert.ToDecimal(temp) + res.Total);
    }

    public struct Service
    {
        public string Name { get; set; }
        public List<Payer> Payers { get; set; }
        public decimal Total { get; set; }

        public Service(string name, IEnumerable<Payer> payers)
        {
            Name = name;
            Payers = payers.ToList();
            Total = Payers.Aggregate(0m, (temp, res) => Convert.ToDecimal(temp) + res.Payment);
        }
    }

    public struct Payer
    {
        public string Name { get; set; }
        public decimal Payment { get; set; }
        public string Date { get; set; }
        public long AccountNumber { get; set; }

        public Payer(string name, decimal payment, DateOnly date, long account)
        {
            Name = name;
            Payment = payment;
            Date = date.ToString();
            AccountNumber = account;
        }
    }
}