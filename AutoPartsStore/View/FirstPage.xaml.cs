using AutoPartsStore.ViewModel;
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

namespace AutoPartsStore.View
{
    /// <summary>
    /// Логика взаимодействия для FirstPage.xaml
    /// </summary>
    public partial class FirstPage : Page
    {
        public FirstPage()
        {
            InitializeComponent();
            WindowProvider.WorkSpacePage = WorkSpacePage;
            WindowProvider.EditDataBaseFrame = EditDataBaseFrame;
            WindowProvider.CategoryFrame = CategoryFrame;
            WindowProvider.ChoiceCarFrame = ChoiceCarFrame;
            WindowProvider.FillUserPage();
        }
       
        
    }

}
