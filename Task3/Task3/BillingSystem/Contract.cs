using Task3.Interface;

namespace Task3
{
    public class Contract
    {

        public ITariff Tariff { get; private set; }
        public Client Client { get; private set; }
        public int PhoneNumber { get; private set; }

        public Contract(Client client, ITariff tariff,int phoneNumber)
        {
            Tariff = tariff;
            Client = client;
            PhoneNumber = phoneNumber;
        }
        
    }
}
