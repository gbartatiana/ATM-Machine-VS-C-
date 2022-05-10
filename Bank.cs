using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankATM
{
    class Bank
    {
        public string BankCode { get; set; }
        public string BankBalance { get; set; }
        public string BankStatus { get; set; }
       

        public string BankInfo
        {
            get
            {
                return BankStatus.ToString();

            }
        }
    }
}
