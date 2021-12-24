using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using Task3.ATE;

namespace Task3.BillingSystem
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
    }
}