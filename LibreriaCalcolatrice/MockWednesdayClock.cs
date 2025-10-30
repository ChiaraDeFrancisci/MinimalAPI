using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaCalcolatrice
{
    public class MockWednesdayClock : IClock
    {
        public DateTime Now()
        {
            return new DateTime(2025, 10, 29);
        }
    }
}
