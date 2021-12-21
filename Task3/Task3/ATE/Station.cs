using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.ArgsEvent;
using Task3.ATE;

namespace Task3
{
    public class Station
    {
        private IDictionary<int, ElementStation> _clientData = new Dictionary<int, ElementStation>();
        public PortController portController { get; set; }

        private int _numberPhoneCounter;

        public event EventHandler<StartingCallEventArgs> AcceptCall;


        public void OnPhoneAcceptingCall(object sender, StartingCallEventArgs args)
        {
            if (_clientData.ContainsKey(args.TargetPhoneNumber))
            {
                var elementStation = _clientData[args.TargetPhoneNumber];
                AcceptCall += elementStation.Port.OnPhoneAcceptingCall;
                elementStation.Port.AcceptCall += elementStation.Phone.OnPhoneAcceptingCall;
                OnAcceptCall(sender, args);
            }
            else
            {
                Console.WriteLine($"Phone [{args.TargetPhoneNumber}] The station did not issue such a number");
            }
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
        }
        public Contract GetContract(Client client, Tariff tariff)
        {

            Contract contract = new Contract(client, tariff, _numberPhoneCounter);
            _numberPhoneCounter++;
            return contract;
        }

        public Phone GetPhone(Contract contract)
        {
            Port newPort = new Port();            
            Phone newPhone = new Phone(newPort, contract.PhoneNumber);
            _clientData.Add(contract.PhoneNumber, new ElementStation(newPort, contract, newPhone));

            newPhone.StartCall += newPort.OnPhoneStartingCall;
            newPort.StartCall += OnPhoneStartingCall;

            

           // newPhone.EndCall += newPort.OnPhoneEndingCall;
            //newPort.EndCall += OnPhoneEndingCall;

            return newPhone;
        }


    }
}
