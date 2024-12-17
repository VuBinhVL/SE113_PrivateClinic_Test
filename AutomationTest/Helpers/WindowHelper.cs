using FlaUI.Core.AutomationElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTest.Helpers
{
    public static class WindowHelper
    {
        public static Window SearchWindowByTitle(Window[] windows, string title)
        {
            Window result = null;
            foreach (var w in windows)
            {
                if (w.Name == title)
                {
                    result = w;
                    break;
                }
            }
            return result;
        }
    }
}
