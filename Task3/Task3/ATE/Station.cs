using System;
using System.Collections.Generic;
using Task3.ArgsEvent;
using Task3.BillingSystem;
using Task3.Interface;
using Task3.ReportCall;

namespace Task3.ATE
{
    public class Station
    {
        private IDictionary<int, ElementStation> _clientData = new Dictionary<int, ElementStation>();
        private IDictionary<Guid, CallInformation> _callInformation = new Dictionary<Guid,CallInformation>();
        private Report _report;
      

        private int _numberPhoneCounter;

        public event EventHandler<CallEventArgs> AcceptCall;
        public event EventHandler<ElementReport> ElementReport;    

        protected virtual void OnElementReport(object sender, ElementReport elementReport)
        {
            ElementReport?.Invoke(sender, elementReport);
        }
  
        private void OnPhoneAcceptingCall(object sender, CallEventArgs args)
        {            

            if (args.statusCall == StatusCall.Call)
            {
     
                if (_clientData.ContainsKey(args.TargetPhoneNumber))
                {
                                       
                    var elementStationTarget = _clientData[args.TargetPhoneNumber];
                    var elementStationSource = _clientData[args.SourcePhoneNumber];
                    if (elementStationTarget.Port.Status == PortStatus.Connected)
                    {                     
                        elementStationTarget.Port.OnPhoneAcceptingCall(sender, args); 
                        CallInformation newCallInformation = new CallInformation(args.SourcePhoneNumber, args.TargetPhoneNumber, DateTime.Now);
                        elementStationSource.Phone.callId = newCallInformation.Id;
                        args.CallId = newCallInformation.Id;
                        _callInformation.Add(newCallInformation.Id, newCallInformation);
                   
                    }
                    else
                    {
                        Console.WriteLine($"Phone [{args.TargetPhoneNumber}] can't get to the station. Port not connected");
                        Console.WriteLine($"Please connect port");
                    }
                    
                }
                else
                {
                    Console.WriteLine($"Phone [{args.TargetPhoneNumber}] The station did not issue such a number");
                    ElementReport newElementReport = new ElementReport(TypeCall.Outgoing, args.SourcePhoneNumber, DateTime.Now, DateTime.Now.Subtract(DateTime.Now), 0);               
                    OnElementReport(this, newElementReport);
                 
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
                    OnElementReport(this, CreateReportElementReport(TypeCall.Outgoing, sourceNumber, date, time, countSeconds* costOneSecondSourceNumber));
                    OnElementReport(this, CreateReportElementReport(TypeCall.Incoming, targetNumber, date, time, 0));
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

        public ElementReport CreateReportElementReport(TypeCall callType, int PhoneNumber,DateTime date, TimeSpan time,int cost)
        {
            ElementReport newElementReport = new ElementReport(callType, PhoneNumber, date, time, cost);
            return newElementReport;
        }

        public void OnPhoneStartingCall(object sender, CallEventArgs args)
        {
            OnPhoneAcceptingCall(this, args);
        }


        public Station()
        {
            _numberPhoneCounter = 6842745;
            _report = new Report();
        }
        public IContract GetContract(Client client, ITariff tariff)
        {

            Contract contract = new Contract(client, tariff, _numberPhoneCounter);
            _numberPhoneCounter++;
            return contract;
        }


        public Phone GetPhone(IContract contract)
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

            newPort.AnswerCall += OnPhoneAcceptingCall;
            return newPhone;
        }


    }
}
