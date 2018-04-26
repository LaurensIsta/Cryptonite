using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptonite.Modules
{
    public class User
    {
        //TODO: Refine Class
        public string name { get; set; }
        public string[] coins { get; set; }
        public int[] amount { get; set; }


        public User(string Name)
        {
            name = Name;
            coins = new string[5];
            amount = new int[5];
        }





    }
}
