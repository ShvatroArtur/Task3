using Task3.BillingSystem;

namespace Task3.Interface
{
    public interface IContract
    {
        ITariff Tariff { get; }
        Client Client { get; }
        int PhoneNumber { get; }
    }
}