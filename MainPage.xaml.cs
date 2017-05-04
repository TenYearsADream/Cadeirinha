using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;
using NewCadeirinhaIoT.Draw;
using NewCadeirinhaIoT.Parameters;
using NewCadeirinhaIoT.PLC;
using Windows.UI.Xaml;
using System.Net;
using NewCadeirinhaIoT.ViewModel;

namespace NewCadeirinhaIoT
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
