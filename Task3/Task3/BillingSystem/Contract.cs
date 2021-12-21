using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Contract
    {

        public Tariff Tariff { get; private set; }
        public Client Client { get; private set; }
        public int PhoneNumber { get; private set; }
        private readonly Random _randomNumber = new Random();

        public Contract(Client client, Tariff tariff,int phoneNumber)
        {
            Tariff = tariff;
            Client = client;
            PhoneNumber = phoneNumber; ;
        }
        
    }
}
