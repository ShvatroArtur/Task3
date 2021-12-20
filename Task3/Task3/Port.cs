using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Port
    {
        public event EventHandler<StartingCallEventArgs> StartCall;
        public PortStatus Status { get; set; }

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

        public void PhoneCallingByStation(string sourcePhoneNumber)
        {
            if (Status == PortStatus.Free)
            {
                Status = PortStatus.Busy;
                OnRequest(this, new StartingCallEventArgs(){SourcePhoneNumber = sourcePhoneNumber });
            }
        }

        public event EventHandler<StartingCallEventArgs> Request;

        protected virtual void OnRequest(object sender, StartingCallEventArgs args)
        {
            Request?.Invoke(sender, args);
        }

    }
}
