using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.ArgsEvent;
using Task3.ATE;

namespace Task3
{
    public class Phone
    {
        public event EventHandler<CallEventArgs> StartCall;
        public event EventHandler<CallEventArgs> AnswerCall;
        public event EventHandler<CallEventArgs> EndCall;


        private Port _port;
        public Guid callId;
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

        public void DropCall()
        {
            if (EndCall != null)
            {
                OnEndCall(this, new CallEventArgs() { statusCall = StatusCall.TheEnd, CallId = callId, HungUpPhoneNumber = PhoneNumber });
            }
        }
        protected virtual void OnEndCall(object sender, CallEventArgs args)
        {
            EndCall?.Invoke(sender, args);
        }
        public void Call(int targetPhoneNumber)
        {
            if (StartCall != null)
            {
                Console.WriteLine($"Phone [{PhoneNumber}] call to phone [{targetPhoneNumber}]");
                OnStartCall(this, new CallEventArgs() { SourcePhoneNumber = PhoneNumber, TargetPhoneNumber = targetPhoneNumber, statusCall = StatusCall.Call });
            }
        }

        protected virtual void OnStartCall(object sender, CallEventArgs args)
        {
            StartCall?.Invoke(sender, args);
        }

        protected virtual void OnAnswerCall(object sender, CallEventArgs args)
        {
            AnswerCall?.Invoke(sender, args);
        }

        public void OnPhoneAcceptingCall(object sender, CallEventArgs args)
        {
            Console.WriteLine($"[ABONENT {args.TargetPhoneNumber}] sees that [ABONENT {args.SourcePhoneNumber}] is calling him");
            callId = args.CallId;
            bool tick = true;
            while (tick == true)
            {
                Console.WriteLine("Answer? Y/N");
                char k = Console.ReadKey().KeyChar;
                if (k == 'Y' || k == 'y')
                {
                    tick = false;
                    args.statusCall = StatusCall.Answer;
                    Console.WriteLine();
                    OnAnswerCall(sender, args);

                }
                else if (k == 'N' || k == 'n')
                {
                    tick = false;
                    Console.WriteLine();
                    args.statusCall = StatusCall.TheEnd;
                    DropCall();
                }
                else
                {
                    tick = true;
                    Console.WriteLine();
                }
            }
        }


        //  public void OnRequest(object sender, StartingCallEventArgs args) { }
    }
}
