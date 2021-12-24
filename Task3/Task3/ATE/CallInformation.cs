using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.ATE
{
    public class CallInformation
    {
        public Guid Id { get; set; }
        public int SourceNumber { get; set; }
        public int TargetNumber { get; set; }
        public DateTime BeginCall { get; set; }
        public DateTime EndCall { get; set; }
        public int Cost { get; set; }

        public CallInformation(int sourceNumber, int targetNumber, DateTime beginCall)
        {
            Id = Guid.NewGuid();
            SourceNumber = sourceNumber;
            TargetNumber = targetNumber;
            BeginCall = beginCall;
        }

    }
}
