using System.Collections.Generic;

namespace Task3.ReportCall
{
    public class Report
    {
        private IList<ElementReport> _itemsReport;

        public void Add(ElementReport item)
        {
            _itemsReport.Add(item);
        }
        public IList<ElementReport> GetElements()
        {
            return _itemsReport;
            
        }
        public Report()
        {
        _itemsReport = new List<ElementReport>();
        }
    }
}
