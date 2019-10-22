﻿using System;
using NGettext;
using Xwt;

namespace R7.Webmate.Xwt
{
    public static class Program
    {
        static void Initialize ()
        {
            Application.Initialize (OSHelper.GetXwtToolkit ());
            TextCatalogKeeper.SetDefault (new Catalog ("R7.Webmate.Xwt", "./resources/locale"));
        }

        [STAThread]
        static void Main ()
        {
            Initialize ();

            var mainWindow = new MainWindow ();
            mainWindow.Show ();

            Application.Run ();
            mainWindow.Dispose ();
        }
    }
}
