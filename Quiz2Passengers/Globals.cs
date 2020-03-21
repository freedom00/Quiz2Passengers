using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz2Passengers
{
    internal class Globals
    {
        public static Database DB { get => Database.GetInstance(); }
    }
}