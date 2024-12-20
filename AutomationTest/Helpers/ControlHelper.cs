﻿using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTest.Helpers
{
    public static class ControlHelper
    {
        public static AutomationElement FindMessageBoxByContent( string content)
        {
            Window[] windows = Program.GetWindowsCur();
            Window window = WindowHelper.SearchWindowByTitle(windows, "YesNoMessageBox");
            if (window == null)
            {
                return null;
            }
            var elementsInWindow = window.FindAllDescendants(cf => cf.ByControlType(ControlType.Text));

            if (elementsInWindow == null)
            {
                return null;
            }

            foreach (var element in elementsInWindow)
            {
                if (element.Properties.Name.Value == content)
                {
                    return element;
                }
            }

            return null;

        }

        public static AutomationElement FindOkMessageBoxByContent(string content)
        {
            Window[] windows = Program.GetWindowsCur();
            Window window = WindowHelper.SearchWindowByTitle(windows, "OkMessageBox");
            if (window == null)
            {
                return null;
            }
            var elementsInWindow = window.FindAllDescendants(cf => cf.ByControlType(ControlType.Text));

            if (elementsInWindow == null)
            {
                return null;
            }

            foreach (var element in elementsInWindow)
            {
                if (element.Properties.Name.Value == content)
                {
                    return element;
                }
            }

            return null;

        }
    }
}
