using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;

namespace Task3
{
    public class Client
    {
        public string FiO { get; }

        private IList<Phone> itemsPhone { get; }

        public Guid Id { get; }

        public Client(string fio)
        {
            Id = new Guid();
            FiO = fio;
            itemsPhone = new List<Phone>();
        }

        public void SetPhone(Phone phone)
        {
            itemsPhone.Add(phone);
        }

        public Phone GetPhone(int number)
        {
            return itemsPhone[number - 1];
        }

        public int GetCountPhones()
        {
            return itemsPhone.Count;
        }

        //public void Call(Phone phone, int targetNumber)
       // {
        //    StartingCallEventArgs args = new StartingCallEventArgs { IdClient = Id };
         //   phone.Call(targetNumber, args);
       // }
    }
}