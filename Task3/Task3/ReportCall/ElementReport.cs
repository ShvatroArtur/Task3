using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.ReportCall
{
    public class ElementReport
    {
        public CallType CallType { get; private set; }
        public int Number { get; private set; }
        public DateTime Date { get; private set; }
        public TimeSpan Time { get; private set; }
        public int Cost { get; private set; }

        public ElementReport(CallType callType, int number, DateTime date, TimeSpan time, int cost)
        {
            CallType = callType;
            Number = number;
            Date = date;
            Time = time;
            Cost = cost;
        }
    }
}
