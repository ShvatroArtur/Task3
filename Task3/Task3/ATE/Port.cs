using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.ArgsEvent;

namespace Task3
{
    public class Port
    {
        public event EventHandler<StartingCallEventArgs> AcceptCall;
        public event EventHandler<StartingCallEventArgs> EndCall;
        public event EventHandler<StartingCallEventArgs> StartCall;
        public event EventHandler<StartingCallEventArgs> AnswerCall;


        public PortStatus Status { get; set; }

        public Port()
        {
            Status = PortStatus.Disabled;
        }
        public void OnPhoneStartingCall(object sender, StartingCallEventArgs args)
        {
            if (Status == PortStatus.Connected)
            {
                OnStartCall(this, args);
            }
            else
            {
                Console.WriteLine($"Phone [{args.SourcePhoneNumber}] can't get to the station. Port not connected");
                Console.WriteLine($"Please connect port");
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

        public void OnPhoneAnsweringCall(object sender, StartingCallEventArgs args)
        {
            OnAnswerCall(sender, args);
        }
        public void OnPhoneAcceptingCall(object sender, StartingCallEventArgs args)
        {
            if (Status == PortStatus.Connected)
            {
                //Status = PortStatus.Busy;
                OnAcceptCall(sender, args);
            }
            else
            {
                Console.WriteLine($"Station can't get to the phone [{args.TargetPhoneNumber}]. Phone  port not connected");
                Console.WriteLine($"Please connect port");
            }
        }
        protected virtual void OnAcceptCall(object sender, StartingCallEventArgs args)
        {
            AcceptCall?.Invoke(sender, args);
        }

        public void OnPhoneEndingCall(object sender, StartingCallEventArgs args)
        {
            if (EndCall != null)
            {
                OnEndCall(this, args);
            }
        }
        protected virtual void OnEndCall(object sender, StartingCallEventArgs args)
        {
            EndCall?.Invoke(sender, args);
        }



        //public void PhoneCallingByStation(int sourcePhoneNumber)
        //{
        //    if (Status == PortStatus.Free)
        //    {
        //        Status = PortStatus.Busy;
        //        OnRequest(this, new StartingCallEventArgs() { SourcePhoneNumber = sourcePhoneNumber });
        //    }
        //}

        //public event EventHandler<StartingCallEventArgs> Request;

        //protected virtual void OnRequest(object sender, StartingCallEventArgs args)
        //{
        //    Request?.Invoke(sender, args);
        //}

    }
}
