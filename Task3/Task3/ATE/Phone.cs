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
        public event EventHandler<StartingCallEventArgs> StartCall;
        public event EventHandler<StartingCallEventArgs> AnswerCall;
        public event EventHandler<StartingCallEventArgs> EndCall;


        private Port _port;
        private Guid _callId;
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
                OnEndCall(this, new StartingCallEventArgs() {statusCall = StatusCall.TheEnd, DropCall = this,CallId = _callId});
            }
        }
        protected virtual void OnEndCall(object sender, StartingCallEventArgs args)
        {
            EndCall?.Invoke(sender, args);
        }
        public void Call(int targetPhoneNumber)
        {
            if (StartCall != null)
            {
                Console.WriteLine($"Phone [{PhoneNumber}] call to phone [{targetPhoneNumber}]");
                OnStartCall(this, new StartingCallEventArgs() { SourcePhoneNumber = PhoneNumber, TargetPhoneNumber = targetPhoneNumber, statusCall = StatusCall.Call });
            }
        }
        public void Answer(StartingCallEventArgs args)
        {
            if (AnswerCall != null)
            {
                args.statusCall = StatusCall.Answer;
                Console.WriteLine($"Phone [{PhoneNumber}] answer to phone [{args.SourcePhoneNumber}]");
                OnStartCall(this, args);
            }
        }



        protected virtual void OnStartCall(object sender, StartingCallEventArgs args)
        {
            StartCall?.Invoke(sender, args);
        }

        protected virtual void OnAnswerCall(object sender, StartingCallEventArgs args)
        {
            AnswerCall?.Invoke(sender, args);
        }



        public void OnPhoneAcceptingCall(object sender, StartingCallEventArgs args)
        {
            Console.WriteLine($"[ABONENT {args.TargetPhoneNumber}] sees that [ABONENT {args.SourcePhoneNumber}] is calling him");
            _callId = args.CallId;
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
