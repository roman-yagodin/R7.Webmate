﻿using System;
using NGettext;
using NLog;
using R7.Webmate.Xwt.Icons;
using R7.Webmate.Xwt.Text;
using Xwt;

namespace R7.Webmate.Xwt
{
    public class MainWindow: Window
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        protected readonly Logger Logger = LogManager.GetCurrentClassLogger ();

        public StatusIcon StatusIcon { get; set; }

        public MainWindow ()
        {
            Icon = IconHelper.GetAppIcon ();

            if (Program.CmdlineArgs.TrayIcon) {
                InitStatusIcon ();
            }

            Width = 600;
            Height = 400;

            CloseRequested += MainWindow_CloseRequested;
            Closed += MainWindow_Closed;

            var notebook = new Notebook ();
            notebook.TabOrientation = NotebookTabOrientation.Right;
            notebook.Add (new TextCleanerWidget (), T.GetString ("Text Cleaner"));
            notebook.Add (new TableCleanerWidget (), T.GetString ("Table Cleaner"));
            notebook.Add (new CaseChangerWidget (), T.GetString ("Case Changer"));
            notebook.Add (new UuidGeneratorWidget (), T.GetString ("UUID Generator"));
            notebook.Add (new ExternalToolsWidget (), T.GetString ("External Tools"));
            notebook.Add (new DecodeHexStringWidget(), T.GetString ("Decode Hex String"));

            notebook.CurrentTabChanged += Notebook_CurrentTabChanged;

            UpdateTitle (notebook.CurrentTab.Label);

            var vbox = new VBox ();
            vbox.PackStart (notebook, true, true);

            Content = vbox;
            Content.Show ();
        }

        void UpdateTitle (string suffix)
        {
            Title = T.GetString ("R7.Webmaster.Xwt - ") + suffix;
        }

        public void InitStatusIcon ()
        {
            try {
                StatusIcon = Application.CreateStatusIcon ();
                StatusIcon.Image = Icon;
                StatusIcon.Menu = BuildStatusMenu ();
            }
            catch (Exception ex) {
                Logger.Warn (ex, "Cannot create status icon - probably not supported by current XWT backend.");
            }
        }

        protected Menu BuildStatusMenu ()
        {
            var miRestore = new MenuItem {
                Label = T.GetString ("Open R7.Webmate"),
                Image = IconHelper.GetIcon ("arrow-up").WithSize (IconSize.Small),
            };
            miRestore.Clicked += MiRestore_Clicked;

            var miClose = new MenuItem {
                Label = T.GetString ("Close"),
                Image = IconHelper.GetIcon ("times-circle").WithSize (IconSize.Small)
            };
            miClose.Clicked += MiClose_Clicked;

            var menu = new Menu ();
            menu.Items.Add (miRestore);
            menu.Items.Add (new SeparatorMenuItem ());
            menu.Items.Add (miClose);

            return menu;
        }

        void MainWindow_CloseRequested (object sender, CloseRequestedEventArgs args)
        {
            if (StatusIcon == null) {
                args.AllowClose = true;
            }
            else {
                args.AllowClose = false;
                Hide ();
            }
        }

        void MainWindow_Closed (object sender, EventArgs args)
        {
            Application.Exit ();
        }

        void MiRestore_Clicked (object sender, EventArgs e)
        {
            Present ();
        }

        void MiClose_Clicked (object sender, EventArgs e)
        {
            Application.Exit ();
        }

        void Notebook_CurrentTabChanged (object sender, EventArgs e)
        {
            var notebook = (Notebook) sender;
            UpdateTitle (notebook.CurrentTab.Label);
        }
    }
}
