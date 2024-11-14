using PrivateClinic.ViewModel.OtherViewModels;
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

namespace PrivateClinic.View.OtherViews
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            LoginViewModel viewModel = new LoginViewModel();
            this.DataContext = viewModel;
        }

        private void FloatingPasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Trigger the login command
                if (DataContext is LoginViewModel viewModel)
                {
                    viewModel.LoginCommand.Execute(null);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // Trigger the login command
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.LoginCommand.Execute(null);
            }

        }
    }
}
