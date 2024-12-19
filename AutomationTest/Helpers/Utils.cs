using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationTest.Helpers
{
    public static class Utils
    {
        public static void Sleep(int time)
        {
            try
            {
                Thread.Sleep(time);
            }
            catch { }
        }
    }
}
