using System;
using Task3.ATE;

namespace Task3
{
    public class StartingCallEventArgs
    {
        public int TargetPhoneNumber { get; set; }
        public int SourcePhoneNumber { get; set; }
        public Guid CallId { get; set; }
        public bool IsAnswer;
        public StatusCall statusCall { get; set; }
        public Phone DropCall { get; set; }
    }
}
