using FlaUI.Core.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTest.Helpers
{
    public static class MouseHelper
    {
        public static void MoveAndLeftClick(int x, int y)
        {
            Mouse.MoveTo(x, y);
            Utils.Sleep(20);
            Mouse.Click(MouseButton.Left);
        }
    }
}
