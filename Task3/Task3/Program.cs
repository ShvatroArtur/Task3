using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Task3.BillingSystem;
using Task3.ReportCall;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            //Phone ph1 = new Phone();
            //Phone ph2 = new Phone();
            //Port port1 = new Port();
            //Port port2 = new Port();
            //Station station = new Station()
            //    { portController = 
            //        new PortController(new List<Port>(new Port[]{port1,port2}))};

            //ph1.StartCall += port1.OnPhoneStartingCall;
            //port1.StartCall += station.OnPhoneStartingCall;

            //ph2.StartCall += port2.OnPhoneStartingCall;
            //port2.StartCall += station.OnPhoneStartingCall;

            //port1.Request += ph1.OnRequest;
            //port2.Request += ph2.OnRequest;
            //ph1.Call("80336852745","102");
            //stat
            Station station = new Station();
            Report report = new Report();
            BillingStation billingStation = new BillingStation(report);

            station.ElementReport += billingStation.OnWritingElementReport;

            Client client1 = new Client("Иванов Иван Иванович");
            Client client2 = new Client("Петров Петр Петрович");

            Contract contract1 = station.GetContract(client1, new Tariff("Light", 10));
            Contract contract2 = station.GetContract(client1, new Tariff("Light2", 15));
            Contract contract3 = station.GetContract(client2, new Tariff("Hard", 20));

            client1.SetPhone(station.GetPhone(contract1));
            client1.SetPhone(station.GetPhone(contract2));
            client2.SetPhone(station.GetPhone(contract3));

            Phone phone1=null;
            Phone phone2=null;
            Phone phone3 = null;
            if (client1.GetCountPhones() == 2)
            {
                phone1 = client1.GetPhone(1);
                phone2 = client1.GetPhone(2);
            }
            if (client2.GetCountPhones() == 1)
            {
                phone3 = client2.GetPhone(1);
            }
            phone1.ConnectPort();
            phone2.ConnectPort();
            phone1.Call(phone2.PhoneNumber);
            Thread.Sleep(2000);
            phone1.DropCall();


            foreach (var elementReport in report.GetElements())
            {
                Console.WriteLine($"Phone number: {elementReport.Number} | Date:{elementReport.Date} | Time(seconds): {elementReport.Time.Seconds} |Cost: {elementReport.Cost} | Call type: {elementReport.CallType}");
            }
            // phone2.DropCall(phone1);
            //Console.WriteLine("123");
        }
    }
}
