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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoPartsStore.View
{
    /// <summary>
    /// Логика взаимодействия для InsecurePassword.xaml
    /// </summary>
    public partial class InsecurePassword : UserControl
    {
        public InsecurePassword()
        {
            InitializeComponent();
        }
        public string InsecurePasswordString
        {
            get { return (string)GetValue(InsecurePasswordStringProperty); }
            set { SetValue(InsecurePasswordStringProperty, value); }
        }

        public static readonly DependencyProperty InsecurePasswordStringProperty =
            DependencyProperty.Register("InsecurePasswordString", typeof(string), typeof(InsecurePassword), new PropertyMetadata(""));

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

            if (this.PasswordEntryBox != null)
            { InsecurePasswordString = PasswordEntryBox.Password; }

        }
    }
}
