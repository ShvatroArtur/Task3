using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.ATE
{
    public class PhoneCall
    {
        private int _sourceNumber;
        private int _targetNumber;
        public StatusCall statusCall;

        public PhoneCall(int sourceNumber, int targetNumber, StatusCall statusCall)
        {
            _sourceNumber = sourceNumber;
            _targetNumber = targetNumber;
            this.statusCall = statusCall;
        }
    }
}
