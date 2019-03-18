﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StartupConfigurerWin10.Entity;

namespace StartupConfigurerWin10.Util
{
    class ShortcutUtil
    {
        public static string StartupPath => System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.StartMenu),
            "Programs",
            "Startup");

        public static IEnumerable<IShortcut> GetCurrentStartup()
        {
            // TODO: impl
            throw new NotImplementedException();
        }

        public static void SaveStartup(IEnumerable<IShortcut> shortcuts)
        {
            // TODO: impl
            // delete & insert ???
        }
    }
}
