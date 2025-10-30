using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaCalcolatrice
{
    class SystemClock : IClock
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
