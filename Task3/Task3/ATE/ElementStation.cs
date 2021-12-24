using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.ATE
{
    public class ElementStation
    {
        public Port Port { get; set; }
        public Contract Contract { get; set; }
        public Phone Phone { get; set; }

        public Guid IdClient { get; set; }
        public int Id { get; }

        public ElementStation(Contract contract, Port port, Phone phone)
        {
            Port = port;
            Contract = contract;
            Id = contract.PhoneNumber;
            Phone = phone;
        }
    }
}
