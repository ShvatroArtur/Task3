using Task3.ReportCall;

namespace Task3.BillingSystem
{
    class BillingStation
    {
        private Report _report;

        public BillingStation(Report report)
        {
            _report = report;
        }
        public void OnWritingElementReport(object sender, ElementReport elementReport)
        {
            _report.Add(elementReport);
        }
    }
}
