using System.Windows;
using CarApp.ViewModel;

namespace CarApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Bruger parameterløs constructor
            DataContext = new CarViewModel();
        }
    }
}
