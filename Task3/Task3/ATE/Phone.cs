using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.ArgsEvent;

namespace Task3
{
    public class Phone
    {
        public event EventHandler<StartingCallEventArgs> StartCall;
        public event EventHandler<EndingCallEventArgs> EndCall;

        private Port _port;
        public int PhoneNumber { get; }
        public Phone(Port port, int phoneNumber)
        {
            _port = port;
            PhoneNumber = phoneNumber;
        }

        public void ConnectPort()
        {
            _port.Status = PortStatus.Connected;
        }

        public void DisablePort()
        {
            _port.Status = PortStatus.Disabled;
        }

        public void DropCall(Phone phone)
        {
            if (EndCall != null)
            {
                OnEndCall(this, new EndingCallEventArgs() { SourcePhoneNumber = phone.PhoneNumber, TargetPhoneNumber = PhoneNumber });
            }
        }
        protected virtual void OnEndCall(object sender, EndingCallEventArgs args)
        {
            EndCall?.Invoke(sender, args);
        }
        public void Call(int targetPhoneNumber)
        {
            if (StartCall != null)
            {
                Console.WriteLine($"Phone [{PhoneNumber}] call to phone [{targetPhoneNumber}]");
                OnStartCall(this, new StartingCallEventArgs() { SourcePhoneNumber = PhoneNumber, TargetPhoneNumber = targetPhoneNumber });
            }
        }

        protected virtual void OnStartCall(object sender, StartingCallEventArgs args)
        {
            StartCall?.Invoke(sender, args);
        }

        public void OnPhoneAcceptingCall(object sender, StartingCallEventArgs args)
        {
            Console.WriteLine($"[ABONENT {args.TargetPhoneNumber}] sees that [ABONENT {args.SourcePhoneNumber}] is calling him");
            Console.WriteLine("Answer?");
        }


        //  public void OnRequest(object sender, StartingCallEventArgs args) { }
    }
}
