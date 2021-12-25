using System;

namespace Task3.ReportCall
{
    public class ElementReport
    {
        public TypeCall CallType { get; private set; }
        public int Number { get; private set; }
        public DateTime Date { get; private set; }
        public TimeSpan Time { get; private set; }
        public int Cost { get; private set; }

        public ElementReport(TypeCall callType, int number, DateTime date, TimeSpan time, int cost)
        {
            CallType = callType;
            Number = number;
            Date = date;
            Time = time;
            Cost = cost;
        }
    }
}
