using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.ArgsEvent;
using Task3.ATE;
using Task3.Interface;
using Task3.ReportCall;

namespace Task3
{
    public class Station
    {
        private IDictionary<int, ElementStation> _clientData = new Dictionary<int, ElementStation>();
        private IDictionary<Guid, CallInformation> _callInformation = new Dictionary<Guid,CallInformation>();
        private Report _report;
        public PortController portController { get; set; }

        private int _numberPhoneCounter;

        public event EventHandler<StartingCallEventArgs> AcceptCall;
        public event EventHandler<ElementReport> ElementReport;

        // public delegate void BindingBillingStation(object sender, ElementReport elementReport);

        protected virtual void OnElementReport(object sender, ElementReport elementReport)
        {
            ElementReport?.Invoke(sender, elementReport);
        }

        public void WriteElementReport(ElementReport elementReport)
        {
            OnElementReport(this, elementReport);
        }

        //public void BindingBillingStationToStation(BindingBillingStation myDelegate)
        // {
        //ElementReport += myDelegate;
        // }
        public void OnPhoneAcceptingCall(object sender, StartingCallEventArgs args)
        {
            if (args.statusCall == StatusCall.Call)
            {
                if (_clientData.ContainsKey(args.TargetPhoneNumber))
                {
                    var elementStation = _clientData[args.TargetPhoneNumber];
                    AcceptCall += elementStation.Port.OnPhoneAcceptingCall;
                    elementStation.Port.AcceptCall += elementStation.Phone.OnPhoneAcceptingCall;

                    elementStation.Phone.AnswerCall += elementStation.Port.OnPhoneAnsweringCall;
                    elementStation.Port.AnswerCall += OnPhoneAcceptingCall;

                    CallInformation newCallInformation = new CallInformation(args.SourcePhoneNumber, args.TargetPhoneNumber, DateTime.Now);
                    args.CallId = newCallInformation.Id;
                    _callInformation.Add(newCallInformation.Id, newCallInformation);

                    OnAcceptCall(sender, args);
                }
                else
                {
                    Console.WriteLine($"Phone [{args.TargetPhoneNumber}] The station did not issue such a number");
                    ElementReport newElementReport = new ElementReport(CallType.Incoming, args.SourcePhoneNumber, DateTime.Now, DateTime.Now.Subtract(DateTime.Now), 0);
                    //add element to Report
                    OnElementReport(this, newElementReport);
                    //_report.Add(newElementReport);
                }

            }
            else if (args.statusCall == StatusCall.Answer)
            {
                Console.WriteLine("Answer");
            }
            else if (args.statusCall == StatusCall.TheEnd)
            {
                if (_callInformation.ContainsKey(args.CallId))
                {
                    _callInformation[args.CallId].EndCall = DateTime.Now;
                   
                    //add element to Report
                    OnElementReport(this, CreateReportElementReport(CallType.Incoming, _callInformation[args.CallId].SourceNumber, _callInformation[args.CallId].BeginCall, _callInformation[args.CallId].BeginCall.Subtract(DateTime.Now) ,0));
                    OnElementReport(this, CreateReportElementReport(CallType.Outgoing, _callInformation[args.CallId].TargetNumber, _callInformation[args.CallId].BeginCall, _callInformation[args.CallId].BeginCall.Subtract(DateTime.Now), 0));
                }

                Console.WriteLine("TheEnd");
            }

        }

        public ElementReport CreateReportElementReport(CallType callType, int PhoneNumber,DateTime data, TimeSpan time,int cost)
        {
            ElementReport newElementReport = new ElementReport(callType, PhoneNumber, data, time, cost);
            return newElementReport;
        }
        protected virtual void OnAcceptCall(object sender, StartingCallEventArgs args)
        {
            AcceptCall?.Invoke(sender, args);
        }

        public void OnPhoneStartingCall(object sender, StartingCallEventArgs args)
        {
            //Console.WriteLine($"Station ");
            //Console.WriteLine("Абонент" + args.SourcePhoneNumber + " к " + args.TargetPhoneNumber);
            OnPhoneAcceptingCall(this, args);
            // While()
        }

        public void OnPhoneEndingCall(object sender, EndingCallEventArgs args)
        {
            //  portController.Items.LastOrDefault().PhoneCallingByStation(args.SourcePhoneNumber);
            Console.WriteLine($"Station ");
            Console.WriteLine("Абонент" + args.TargetPhoneNumber + "повесил трубку");
            //OnPhoneAcceptingCall(this, args);
            // While()
        }

        public Station()
        {
            _numberPhoneCounter = 6842745;
            _report = new Report();
        }
        public Contract GetContract(Client client, ITariff tariff)
        {

            Contract contract = new Contract(client, tariff, _numberPhoneCounter);
            _numberPhoneCounter++;
            return contract;
        }

        public bool onFreePhoneNumber(int phoneNumber)
        {
            if (_clientData.ContainsKey(phoneNumber))
            {
                return false;
            }
            return true;
        }
        public Phone GetPhone(Contract contract)
        {

            Guid id = contract.Client.Id;
            int phoneNumber = contract.PhoneNumber;
            Port newPort = new Port();
            Phone newPhone = new Phone(newPort, phoneNumber);

            _clientData.Add(phoneNumber, new ElementStation(contract, newPort, newPhone));

            newPhone.StartCall += newPort.OnPhoneStartingCall;
            newPort.StartCall += OnPhoneStartingCall;

            newPhone.EndCall += newPort.OnPhoneEndingCall;
            newPort.EndCall += OnPhoneStartingCall;

            return newPhone;
        }


    }
}
