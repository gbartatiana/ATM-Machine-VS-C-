using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankATM
{

    class Client
    {
        public string AccountDescription { get; set; }
        public string AccountId { get; set; }
        public string ClientId { get; set; }
        public string FullName { get; set; }
        public string ClientSituation { get; set; }



        public string ClientFullInfo
        {
            get
            {
                return ClientId.ToString();
             
            }
        }

    }
}
    



