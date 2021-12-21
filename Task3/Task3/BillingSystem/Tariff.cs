namespace Task3
{
    public class Tariff
    {
        public string Name { get; private set; }
        public int CostMinute { get; private set; }

        public Tariff(string name, int costMinute)
        {
            Name = name;
            CostMinute = costMinute;

        }
    }
}