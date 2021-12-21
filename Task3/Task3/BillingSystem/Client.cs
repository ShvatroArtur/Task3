using System.Security.Permissions;

namespace Task3
{
    public class Client
    {
        public string FiO { get; }

        public Client(string fio)
        {
            FiO = fio;
        }
    }
}