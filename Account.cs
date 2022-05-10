using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankATM
{
    class Account
    {
        public string ClientId { get; set; }
        public string AccountId { get; set; }
        public string AccountDescription { get; set; }
        public string AccountBalance{ get; set; }



        public string AccountFullInfo
        {
            get
            {
                return AccountDescription.ToString().Trim() + " , " + AccountId.ToString().Trim();
            }
        }

        public string AccountFullInfo2
        {
            get
            {
                return ClientId.Trim() + "," + AccountId;
            }
        }
    }
}
