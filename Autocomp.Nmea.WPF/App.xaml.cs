using Autocomp.Nmea.Common;
using Autocomp.Nmea.Common.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Autocomp.Nmea.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            INmeaMessageParseService parseService = new NmeaMessageParseService();

            Window window = new MainWindow
            {
                DataContext = new ParseViewModel(parseService)
            };
            window.Show();

            base.OnStartup(e);
        }
    }
}
