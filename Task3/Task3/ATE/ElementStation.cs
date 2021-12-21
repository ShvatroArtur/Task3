using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.ATE
{
    public class ElementStation
    {
        public Port Port { get; private set; }
        public Contract Contract { get; private set; }
        public Phone Phone { get; private set; }

        public ElementStation(Port port, Contract contract, Phone phone)
        {
            Port= port;
            Contract = contract;
            Phone = phone;
        }
    }
}
