using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW10
{
    public static  class Counfiguer
    {
        public static string Connectionstring { get; set; }
        static Counfiguer()
        {
            Connectionstring = @"Data Source=LEILI\LEILA;Initial Catalog=CW10;Integrated Security=true;";
        }
    }
}
