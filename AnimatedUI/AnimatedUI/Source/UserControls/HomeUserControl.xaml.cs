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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnimatedUI.Source.UserControls
{
    /// <summary>
    /// Логика взаимодействия для HomeViewUserControl.xaml
    /// </summary>
    public partial class HomeViewUserControl : UserControl
    {
        private bool _isTwoWayGridOpen = false;
        private bool _isOnlyUpGridOpen = false;
        private bool _isOnlyBottomGridOpen = false;
        public HomeViewUserControl()
        {
            InitializeComponent();
        }

        private DoubleAnimation CreateDoubleAnimation(double from,double to,Duration duration) 
        {
            var animator = new DoubleAnimation();
            animator.From = from;
            animator.To = to;
            animator.Duration = duration;
            return animator;
        }

        private void TwoWayGridStartOnClicked(object sender, RoutedEventArgs e)
        {
            var animator = !_isTwoWayGridOpen
                ? CreateDoubleAnimation(TwoWayGrid.ActualHeight, TwoWayGrid.ActualHeight + 200, TimeSpan.FromSeconds(1))
                : CreateDoubleAnimation(TwoWayGrid.ActualHeight, TwoWayGrid.ActualHeight - 200, TimeSpan.FromSeconds(1));
            TwoWayGrid.BeginAnimation(HeightProperty,animator);
            _isTwoWayGridOpen = !_isTwoWayGridOpen;

        }

        private void OnlyUpGridStartOnClicked(object sender, RoutedEventArgs e)
        {
            var animator = !_isOnlyUpGridOpen
                ? CreateDoubleAnimation(OnlyUpGrid.ActualHeight, OnlyUpGrid.ActualHeight + 100, TimeSpan.FromSeconds(1))
                : CreateDoubleAnimation(OnlyUpGrid.ActualHeight, OnlyUpGrid.ActualHeight - 100, TimeSpan.FromSeconds(1));
            OnlyUpGrid.BeginAnimation(HeightProperty, animator);
            _isOnlyUpGridOpen = !_isOnlyUpGridOpen;
        }

        private void OnlyBottomGridStartOnClicked(object sender, RoutedEventArgs e)
        {
            var animator = !_isOnlyBottomGridOpen
                ? CreateDoubleAnimation(OnlyBottomGrid.ActualHeight, OnlyBottomGrid.ActualHeight + 100, TimeSpan.FromSeconds(1))
                : CreateDoubleAnimation(OnlyBottomGrid.ActualHeight, OnlyBottomGrid.ActualHeight - 100, TimeSpan.FromSeconds(1));
            OnlyBottomGrid.BeginAnimation(HeightProperty, animator);
            _isOnlyBottomGridOpen = !_isOnlyBottomGridOpen;
        }
    }
}
