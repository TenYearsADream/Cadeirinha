using Windows.UI.Xaml.Controls;
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
