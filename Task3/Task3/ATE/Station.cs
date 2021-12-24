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
      

        private int _numberPhoneCounter;

        public event EventHandler<CallEventArgs> AcceptCall;
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
        public void OnPhoneAcceptingCall(object sender, CallEventArgs args)
        {
            

            if (args.statusCall == StatusCall.Call)
            {
     
                if (_clientData.ContainsKey(args.TargetPhoneNumber))
                {
                    var elementStationTarget = _clientData[args.TargetPhoneNumber];
                    var elementStationSource = _clientData[args.SourcePhoneNumber];
                    AcceptCall += elementStationTarget.Port.OnPhoneAcceptingCall;
                    elementStationTarget.Port.AcceptCall += elementStationTarget.Phone.OnPhoneAcceptingCall;

                    elementStationTarget.Phone.AnswerCall += elementStationTarget.Port.OnPhoneAnsweringCall;
                    elementStationTarget.Port.AnswerCall += OnPhoneAcceptingCall;

                    CallInformation newCallInformation = new CallInformation(args.SourcePhoneNumber, args.TargetPhoneNumber, DateTime.Now);

                    elementStationSource.Phone.callId = newCallInformation.Id;
                    args.CallId = newCallInformation.Id;
                    _callInformation.Add(newCallInformation.Id, newCallInformation);

                    OnAcceptCall(sender, args);
                }
                else
                {
                    Console.WriteLine($"Phone [{args.TargetPhoneNumber}] The station did not issue such a number");
                    ElementReport newElementReport = new ElementReport(CallType.Outgoing, args.SourcePhoneNumber, DateTime.Now, DateTime.Now.Subtract(DateTime.Now), 0);
                    //add element to Report
                    OnElementReport(this, newElementReport);
                    //_report.Add(newElementReport);
                }

            }
            else if (args.statusCall == StatusCall.Answer)
            {
                Console.WriteLine($"[ABONENT {args.TargetPhoneNumber}] answers to [ABONENT {args.SourcePhoneNumber}]");
                Console.WriteLine("They are talking");
            }
            else if (args.statusCall == StatusCall.TheEnd)
            {
                if (_callInformation.ContainsKey(args.CallId))
                {
                    _callInformation[args.CallId].EndCall = DateTime.Now;
                    var sourceNumber = _callInformation[args.CallId].SourceNumber;
                    var targetNumber = _callInformation[args.CallId].TargetNumber;
                    var date = _callInformation[args.CallId].BeginCall;
                    var time = _callInformation[args.CallId].EndCall.Subtract(_callInformation[args.CallId].BeginCall);
                    var countSeconds = time.Seconds;
                    var costOneSecondSourceNumber = GetCostOneSecond(sourceNumber);

                    Console.WriteLine($"[ABONENT {args.HungUpPhoneNumber}] hung up");
                   
                    //add element to Report
                    OnElementReport(this, CreateReportElementReport(CallType.Outgoing, sourceNumber, date, time, countSeconds* costOneSecondSourceNumber));
                    OnElementReport(this, CreateReportElementReport(CallType.Incoming, targetNumber, date, time, 0));
                    _callInformation.Remove(args.CallId);
                   
                }
                
            }

        }

        private int GetCostOneSecond(int phoneNumber)
        {
            if (_clientData.ContainsKey(phoneNumber))
            {
                return _clientData[phoneNumber].Contract.Tariff.CostOneSecond;
            }
            return 0;
        }

        public ElementReport CreateReportElementReport(CallType callType, int PhoneNumber,DateTime date, TimeSpan time,int cost)
        {
            ElementReport newElementReport = new ElementReport(callType, PhoneNumber, date, time, cost);
            return newElementReport;
        }
        protected virtual void OnAcceptCall(object sender, CallEventArgs args)
        {
            AcceptCall?.Invoke(sender, args);
        }

        public void OnPhoneStartingCall(object sender, CallEventArgs args)
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
