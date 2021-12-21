using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.ATE
{
    public class BindingPortToContract
    {
        private Port _port;
        private Contract _contract;

        public BindingPortToContract(Port port, Contract contract)
        {
            _port = port;
            _contract = contract;
        }
    }
}
