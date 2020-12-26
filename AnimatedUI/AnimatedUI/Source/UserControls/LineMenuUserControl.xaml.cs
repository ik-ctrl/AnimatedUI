using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnimatedUI.Source.UserControls
{
    /// <summary>
    /// Логика взаимодействия для LineMenuUserControl.xaml
    /// </summary>
    public partial class LineMenuUserControl : UserControl
    {
        public LineMenuUserControl()
        {
            InitializeComponent();
        }

        public LineUserControl Line;
        public int Mode = 1;
        public int Delay = 0;
        public int Speed = 300;

        private readonly List<LineUserControl> _lines = new List<LineUserControl>();
        private Thread _animation;



        private void StartAnimation_OnClick(object sender, RoutedEventArgs e)
        {
            StopAnima();
            MainCanvas.IsEnabled = false;

            LineUserControl.CompleteAnimationCallback _callback = delegate (LineUserControl line)
                {
                    Task.Run(delegate ()
                    {
                        _animation = Thread.CurrentThread;
                        foreach (var luc in _lines)
                        {
                            if (_animation == null)
                                return;
                            Thread.Sleep(Delay);
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                luc.SetSpeed(new TimeSpan(0, 0, 0, 0, Speed));
                                luc.BeginAnimation();
                            });
                        }

                    });
                };
            _lines.Last().CompleteAnimationEvent += _callback;

            foreach (var line in _lines)
            {
                line.Line1.X1 = line.Line1.Y1 = line.Line1.X2 = line.Line1.Y2 = 0;
            }

            _callback(null);
        }

        /// <summary>
        /// todo :https://www.youtube.com/watch?v=y3979ahvQAQ&t=1329s&ab_channel=TEAMDFU 21:08
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopAnimation_OnClick(object sender, RoutedEventArgs e)
        {
            StopAnima();
        }

        private void StopAnima()
        {
            _animation?.Abort();
            _animation = null;

            foreach (var line in _lines)
            {
                line.StopAnimation();
                line.RemoveAllHandles_CompleteAnimationEvent();
            }

            MainCanvas.IsEnabled = true;
        }

        private void ModeChange_Click(object sender, RoutedEventArgs e = null)
        {
            var tb = sender as ToggleButton;
            var parentGrid = tb.Parent as UniformGrid;
            foreach (ToggleButton child in parentGrid.Children)
            {
                child.IsChecked = false;
            }

            tb.IsChecked = true;
            Mode = Convert.ToInt32(tb.Tag);
            MainCanvas_MouseMove(null,null);
        }

        private void TextBox_Speed_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Speed = ((sender as TextBox).Text == string.Empty) ? 0 : Convert.ToInt32((sender as TextBox).Text);
        }

        private void TextBox_Delay_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Delay = ((sender as TextBox).Text == string.Empty) ? 0 : Convert.ToInt32((sender as TextBox).Text);
        }

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            SetLinePosition(Mode);
        }

        private void MainCanvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Line = new LineUserControl()
            {
                X1 = (Line == null) ? Mouse.GetPosition(this).X - MainCanvas.Margin.Left : Line.X2,
                Y1 = (Line == null) ? Mouse.GetPosition(this).Y - MainCanvas.Margin.Top : Line.Y2,
                X2 = Mouse.GetPosition(this).X - MainCanvas.Margin.Left,
                Y2 = Mouse.GetPosition(this).Y - MainCanvas.Margin.Top,
            };
            Line.Line1.StrokeThickness = 1;
            _lines.Add(Line);
            MainCanvas.Children.Add(Line);
        }

        private void MainCanvas_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetLinePosition(Mode);
            Line = null;
        }


        private void SetLinePosition(int mode)
        {
            if (Line == null)
                return;
            switch (mode)
            {
                case 1:
                    Line.X2 = Mouse.GetPosition(this).X - MainCanvas.Margin.Left;
                    Line.Y2 = Mouse.GetPosition(this).Y - MainCanvas.Margin.Top;
                    break;

                case 2:
                    Line.X2 = Mouse.GetPosition(this).X - MainCanvas.Margin.Left;
                    Line.Y2 = Line.Y1 - (Line.X1 - Mouse.GetPosition(this).X + MainCanvas.Margin.Left);
                    break;

                case 3:
                    Line.X2 = Mouse.GetPosition(this).X - MainCanvas.Margin.Left;
                    Line.Y2 = Line.Y1 + (Line.X1 - Mouse.GetPosition(this).X + MainCanvas.Margin.Left);
                    break;

                case 4:
                    Line.X2 = Line.X2;
                    Line.Y2 = Mouse.GetPosition(this).Y - MainCanvas.Margin.Top;
                    break;

                case 5:
                    Line.X2 = Mouse.GetPosition(this).X - MainCanvas.Margin.Left;
                    Line.Y2 = Line.Y1;
                    break;

            }
        }

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Source.GetType() == typeof(TextBox) ||
                    (!Keyboard.IsKeyDown(Key.LeftCtrl) && !Keyboard.IsKeyDown(Key.RightCtrl)))
                    return;

                var modeNumber = e.Key.ToString()[1].ToString();
                Mode = Convert.ToInt32(modeNumber);
                ModeChange_Click(UG_Modes.Children[Mode - 1]);
            }
            catch (Exception exception)
            {
                //ignore
            }
            finally
            {
                MainCanvas_MouseMove(null, null);
            }

        }

        private void TextBox_TextInput(object sender, TextCompositionEventArgs e)
        {

            if(sender.GetType()!=typeof(TextBox)) return;
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
