using Task3.Interface;
namespace Task3.ATE
{
    public class ElementStation
    {
        public Port Port { get; set; }
        public IContract Contract { get; set; }
        public Phone Phone { get; set; }
        public int Id { get; }

        public ElementStation(IContract contract, Port port, Phone phone)
        {
            Port = port;
            Contract = contract;
            Id = contract.PhoneNumber;
            Phone = phone;
        }
    }
}
