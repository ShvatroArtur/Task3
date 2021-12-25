using System;
using System.Threading;
using Task3.ATE;
using Task3.BillingSystem;
using Task3.ReportCall;
using Task3.Interface;
namespace Task3
{
    class Program
    {
        static void Show(ElementReport elementReport)
        {
            Console.WriteLine($"Phone number: {elementReport.Number} | Date:{elementReport.Date} | Time(seconds): {elementReport.Time.Seconds} |Cost: {elementReport.Cost} | Call type: {elementReport.CallType}");
        }

        static void Main(string[] args)
        {
            Station station = new Station();
            Report report = new Report();
            BillingStation billingStation = new BillingStation(report);

            station.ElementReport += billingStation.OnWritingElementReport;

            Client client1 = new Client("Иванов Иван Иванович");
            Client client2 = new Client("Петров Петр Петрович");
            Client client3 = new Client("Сидоров Иван Иванович");

            IContract contract1 = station.GetContract(client1, new Tariff("Light", 10));
            IContract contract2 = station.GetContract(client1, new Tariff("Light2", 15));
            IContract contract3 = station.GetContract(client2, new Tariff("Hard", 20));
            IContract contract4 = station.GetContract(client3, new Tariff("Hard", 20));

            client1.SetPhone(station.GetPhone(contract1));
            client1.SetPhone(station.GetPhone(contract2));
            client2.SetPhone(station.GetPhone(contract3));
            client3.SetPhone(station.GetPhone(contract4));

            Phone phone1=null;
            Phone phone2=null;
            Phone phone3 = null;
            Phone phone4 = null;

            if (client1.GetCountPhones() == 2)
            {
                phone1 = client1.GetPhone(1);
                phone2 = client1.GetPhone(2);
            }
            if (client2.GetCountPhones() == 1)
            {
                phone3 = client2.GetPhone(1);
            }
            if (client3.GetCountPhones() == 1)
            {
                phone4 = client3.GetPhone(1);
            }
            phone1.ConnectPort();
            phone2.ConnectPort();
            phone4.ConnectPort();
         
            phone2.Call(phone3.PhoneNumber);
            Console.WriteLine("-----------------------------------------------");
            phone1.Call(phone2.PhoneNumber);
            Thread.Sleep(2000);
            phone1.DropCall();
            Console.WriteLine("-----------------------------------------------");
            phone4.Call(phone1.PhoneNumber);
            Thread.Sleep(2000);
            phone4.DropCall();
            Console.WriteLine("-----------------------------------------------");

            foreach (var elementReport in report.GetElements())
            {
                Show(elementReport);
            }

            Console.WriteLine();
            Console.WriteLine("Filter by cost");
            foreach (var elementReport in report.FilterCost(1, 150, report.GetElements()))
            {
                Show(elementReport);
            }

            Console.WriteLine("Filter by number");
            foreach (var elementReport in report.FilterAbonent(6842745, report.GetElements()))
            {
                Show(elementReport);
            }

            Console.WriteLine("Filter by date");
            foreach (var elementReport in report.FilterDate(DateTime.Now.AddDays(-1),DateTime.Now, report.GetElements()))
            {
                Show(elementReport);
            }

        }
    }
}
