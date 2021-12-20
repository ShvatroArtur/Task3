using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Phone
    {
        public event EventHandler<StartingCallEventArgs> StartCall;

        public void Call(string  sourcePhoneNumber, string targetPhoneNumber)
        {
            OnStartCall(this, new StartingCallEventArgs(){SourcePhoneNumber = sourcePhoneNumber, TargetPhoneNumber = targetPhoneNumber});
        }

        protected virtual void OnStartCall(object sender, StartingCallEventArgs args)
        {
            StartCall?.Invoke(sender,args);
        }

        public void OnRequest(object sender, StartingCallEventArgs args) { }
    }
}
