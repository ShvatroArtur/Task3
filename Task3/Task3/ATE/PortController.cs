using System.Collections;
using System.Collections.Generic;

namespace Task3
{
    public class PortController
    {
        private IEnumerable<Port> _items;
        public IEnumerable<Port> Items
        {
            get { return _items; }
        }

        public PortController(IEnumerable<Port> items)
         {
           _items = items;
        }
    }
}