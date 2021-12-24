using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<ElementReport> FilterCost(int costMin, int costMax, IList<ElementReport> itemsReport)
        {
            return itemsReport.Where(x => (x.Cost >= costMin) && (x.Cost <= costMax));

        }
        
        public IEnumerable<ElementReport> FilterDate(DateTime dateMin, int dateMax, IList<ElementReport> itemsReport)
        {
            return itemsReport.Where(x => (x.Cost >= costMin) && (x.Cost <= costMax));

        }
        //  public IList<ElementReport>  FilterCalls(TypeFilter filter, IList<ElementReport> itemsReport)
        // {

        //            itemsReport.Where(x=>x.Cost>=)
        //       }
    }
}
