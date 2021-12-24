using Task3.Interface;

namespace Task3
{
    public class Tariff : ITariff
    {
        public string Name { get; private set; }
        public int CostOneSecond { get; private set; }

        public Tariff(string name, int costOneSecond)
        {
            Name = name;
            CostOneSecond = costOneSecond;

        }
    }
}