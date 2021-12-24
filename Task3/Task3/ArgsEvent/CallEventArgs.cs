using System;
using Task3.ATE;

namespace Task3.ArgsEvent
{
    public class CallEventArgs
    {
        public int TargetPhoneNumber { get; set; }
        public int SourcePhoneNumber { get; set; }
        public int HungUpPhoneNumber { get; set; }
        public Guid CallId { get; set; }
        public bool IsAnswer;
        public StatusCall statusCall { get; set; }
    
    }
}
