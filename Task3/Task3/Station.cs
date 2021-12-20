using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Station
    {
        public PortController portController { get; set; }
        
        public void OnPhoneStartingCall(object sender, StartingCallEventArgs args)
        {
            portController.Items.LastOrDefault().PhoneCallingByStation(args.SourcePhoneNumber);
            Console.WriteLine("Звонит "+ args.SourcePhoneNumber+ " к "+args.TargetPhoneNumber);
        }

    }
}
