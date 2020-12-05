using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Controls.Primitives;
using AnimatedUI.Source.Infrastructure;
using AnimatedUI.Source.ViewModels;

namespace AnimatedUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = new NavigationViewModel();
            vm.SelectedViewModel = new HomeViewModel();
            this.DataContext = vm;
        }

        #region Mouse event
        private void HomeButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Uc_Popup.PlacementTarget = HomeButton;
            Uc_Popup.Placement = PlacementMode.Left;
            Uc_Popup.IsOpen = true;
            MenuItemHeader.PopupText.Text = "Домой";
        }

        private void HomeButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Uc_Popup.Visibility = Visibility.Collapsed;
            Uc_Popup.IsOpen = false;

        }

        private void RainButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Uc_Popup.PlacementTarget = RainButton;
            Uc_Popup.Placement = PlacementMode.Left;
            Uc_Popup.IsOpen = true;
            MenuItemHeader.PopupText.Text = "Дождик";
        }


        private void RainButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Uc_Popup.Visibility = Visibility.Collapsed;
            Uc_Popup.IsOpen = false;
        }

        private void SettingsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Uc_Popup.PlacementTarget = SettingsButton;
            Uc_Popup.Placement = PlacementMode.Left;
            Uc_Popup.IsOpen = true;
            MenuItemHeader.PopupText.Text = "Настроечки";
        }


        private void SettingsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Uc_Popup.Visibility = Visibility.Collapsed;
            Uc_Popup.IsOpen = false;
        }

        #endregion

        #region Window action 
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion
    }
}
