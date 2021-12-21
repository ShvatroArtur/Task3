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
        private IDictionary<int, BindingPortToContract> _clientData = new Dictionary<int, BindingPortToContract>();
        public PortController portController { get; set; }

        private int _numberPhoneCounter;

        public event EventHandler<StartingCallEventArgs> AcceptCall;


        public void OnPhoneAcceptingCall(object sender, StartingCallEventArgs args)
        {
            OnAcceptCall(sender, args);
        }

        protected virtual void OnAcceptCall(object sender, StartingCallEventArgs args)
        {
            AcceptCall?.Invoke(sender, args);
        }

        public void OnPhoneStartingCall(object sender, StartingCallEventArgs args)
        {
            //  portController.Items.LastOrDefault().PhoneCallingByStation(args.SourcePhoneNumber);
            Console.WriteLine($"Station ");
            Console.WriteLine("Абонент" + args.SourcePhoneNumber + " к " + args.TargetPhoneNumber);
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
            _clientData.Add(contract.PhoneNumber, new BindingPortToContract(newPort, contract));
            Phone newPhone = new Phone(newPort, contract.PhoneNumber);

            newPhone.StartCall += newPort.OnPhoneStartingCall;
            newPort.StartCall += OnPhoneStartingCall;

            AcceptCall += newPort.OnPhoneAcceptingCall;
            newPort.AcceptCall += newPhone.OnPhoneAcceptingCall;

            newPhone.EndCall += newPort.OnPhoneEndingCall;
            newPort.EndCall += OnPhoneEndingCall;

            return newPhone;
        }


    }
}
