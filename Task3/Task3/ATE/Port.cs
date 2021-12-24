using System;

namespace Task3
{
    public class Port
    {
        public event EventHandler<CallEventArgs> AcceptCall;
        public event EventHandler<CallEventArgs> EndCall;
        public event EventHandler<CallEventArgs> StartCall;
        public event EventHandler<CallEventArgs> AnswerCall;


        public PortStatus Status { get; set; }

        public Port()
        {
            Status = PortStatus.Disabled;
        }
        public void OnPhoneStartingCall(object sender, CallEventArgs args)
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

        protected virtual void OnStartCall(object sender, CallEventArgs args)
        {
            StartCall?.Invoke(sender, args);
        }

        protected virtual void OnAnswerCall(object sender, CallEventArgs args)
        {
            AnswerCall?.Invoke(sender, args);
        }

        public void OnPhoneAnsweringCall(object sender, CallEventArgs args)
        {
            OnAnswerCall(sender, args);
        }
        public void OnPhoneAcceptingCall(object sender, CallEventArgs args)
        {
            if (Status == PortStatus.Connected)
            {
                OnAcceptCall(sender, args);
            }
            else
            {
                Console.WriteLine($"Station can't get to the phone [{args.TargetPhoneNumber}]. Phone  port not connected");
                Console.WriteLine($"Please connect port");
            }
        }
        protected virtual void OnAcceptCall(object sender, CallEventArgs args)
        {
            AcceptCall?.Invoke(sender, args);
        }

        public void OnPhoneEndingCall(object sender, CallEventArgs args)
        {
            if (EndCall != null)
            {
                OnEndCall(this, args);
            }
        }
        protected virtual void OnEndCall(object sender, CallEventArgs args)
        {
            EndCall?.Invoke(sender, args);
        }
    }
}
