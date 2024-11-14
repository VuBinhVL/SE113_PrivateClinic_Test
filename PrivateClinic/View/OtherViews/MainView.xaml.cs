using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PrivateClinic.ViewModel.OtherViewModels;

namespace PrivateClinic.View.OtherViews
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            MainViewModel viewModel = new MainViewModel();
            this.DataContext = viewModel;
        }
        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.blurPanel.Opacity = 0.2;
            this.blurPanel.Visibility = Visibility.Visible;
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            this.blurPanel.Visibility = Visibility.Hidden;
            this.blurPanel.Opacity = 0;
        }

    }
}
