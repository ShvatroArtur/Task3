using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class Contract
    {
        public Tariff Tariff { get; private set; }
        public Client Client{ get; private set; }
        public int Number { get; private set; }
        private readonly Random _randomNumber = new Random();

        public Contract(Tariff tariff, Client client)
        {
            Tariff = tariff;
            Client = client;
            Number = _randomNumber.Next(1000000, 9999999); ;
        }
    }
}
