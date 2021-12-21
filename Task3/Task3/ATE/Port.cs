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
        public event EventHandler<EndingCallEventArgs> EndCall;
        public event EventHandler<StartingCallEventArgs> StartCall;


        public PortStatus Status { get; private set; }

        public Port()
        {
            Status = PortStatus.Free;
        }
        public void OnPhoneStartingCall(object sender, StartingCallEventArgs args)
        {
            if (Status == PortStatus.Free)
            {
                Status = PortStatus.Busy;
                OnStartCall(this, args);
            }
        }

        protected virtual void OnStartCall(object sender, StartingCallEventArgs args)
        {
            StartCall?.Invoke(sender, args);
        }

        public void OnPhoneAcceptingCall(object sender, StartingCallEventArgs args)
        {
            if (Status == PortStatus.Free)
            {
                Status = PortStatus.Busy;
                OnAcceptCall(sender, args);
            }
        }
        protected virtual void OnAcceptCall(object sender, StartingCallEventArgs args)
        {
            AcceptCall?.Invoke(sender, args);
        }

        public void OnPhoneEndingCall(object sender, EndingCallEventArgs args)
        {
            if (EndCall != null)
            {
                OnEndCall(this, args);
            }
        }
        protected virtual void OnEndCall(object sender, EndingCallEventArgs args)
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
