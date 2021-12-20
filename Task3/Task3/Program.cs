using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Phone ph1 = new Phone();
            Phone ph2 = new Phone();
            Port port1 = new Port();
            Port port2 = new Port();
            Station station = new Station()
                { portController = 
                    new PortController(new List<Port>(new Port[]{port1,port2}))};

            ph1.StartCall += port1.OnPhoneStartingCall;
            port1.StartCall += station.OnPhoneStartingCall;

            ph2.StartCall += port2.OnPhoneStartingCall;
            port2.StartCall += station.OnPhoneStartingCall;

            port1.Request += ph1.OnRequest;
            port2.Request += ph2.OnRequest;
            ph1.Call("80336852745","102");
            //stat
        }
    }
}
