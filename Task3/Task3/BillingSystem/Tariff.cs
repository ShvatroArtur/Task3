using Task3.Interface;

namespace Task3
{
    public class Tariff : ITariff
    {
        public string Name { get; private set; }
        public int CostSecond { get; private set; }

        public Tariff(string name, int costSecond)
        {
            Name = name;
            CostSecond = costSecond;

        }
    }
}