using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoPartsStore.View.Product
{
    /// <summary>
    /// Логика взаимодействия для SpoilerUserControl111111.xaml
    /// </summary>
    public partial class SpoilerUserControl111 : UserControl
    {
        public SpoilerUserControl111()
        {
            InitializeComponent();
        }
        public object SpoilerContent
        {
            get { return (object)GetValue(SpoilerContentProperty); }
            set { SetValue(SpoilerContentProperty, value); }
        }

        public static readonly DependencyProperty SpoilerContentProperty =
            DependencyProperty.Register("SpoilerContent", typeof(object), typeof(SpoilerUserControl111), new PropertyMetadata(0));

        public string ButtonTextWhenHide
        {
            get { return (string)GetValue(ButtonTextWhenHideProperty); }
            set { SetValue(ButtonTextWhenHideProperty, value); }
        }

        public static readonly DependencyProperty ButtonTextWhenHideProperty =
            DependencyProperty.Register("ButtonTextWhenHide", typeof(string), typeof(SpoilerUserControl111), new PropertyMetadata(""));


        public string ButtonTextWhenShow
        {
            get { return (string)GetValue(ButtonTextWhenShowProperty); }
            set { SetValue(ButtonTextWhenShowProperty, value); }
        }

        public static readonly DependencyProperty ButtonTextWhenShowProperty =
            DependencyProperty.Register("ButtonTextWhenShow", typeof(string), typeof(SpoilerUserControl111), new PropertyMetadata(""));

        private void Spoiler_Click(object sender, RoutedEventArgs e)
        {
            if (spoilerGrid.Visibility == Visibility.Visible)
            {
                contentGrid.Visibility = Visibility.Visible;
                spoilerGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                contentGrid.Visibility = Visibility.Collapsed;
                spoilerGrid.Visibility = Visibility.Visible;
            }
        }
    }
}
