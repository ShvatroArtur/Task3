namespace Task3
{
    public class Tariff
    {
        public NameTariff Name { get; private set; }
        public int CostMinute { get; private set; }

        public Tariff(NameTariff name, int costMinute)
        {
            Name = name;
            CostMinute = costMinute;

        }
    }
}