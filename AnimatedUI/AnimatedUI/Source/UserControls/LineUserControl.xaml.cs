using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DoubleAnimationUsingKeyFrames = System.Windows.Media.Animation.DoubleAnimationUsingKeyFrames;

namespace AnimatedUI.Source.UserControls
{
    /// <summary>
    /// Логика взаимодействия для LineUserControl.xaml
    /// </summary>
    public partial class LineUserControl : UserControl
    {
        public LineUserControl()
        {
            InitializeComponent();
        }

        static LineUserControl()
        {
            X1prop = DependencyProperty.Register(nameof(X1), typeof(double), typeof(LineUserControl), new PropertyMetadata(0.0d, X1_PropCallback));
            Y1prop = DependencyProperty.Register(nameof(Y1), typeof(double), typeof(LineUserControl), new PropertyMetadata(0.0d, Y1_PropCallback)); ;
            X2prop = DependencyProperty.Register(nameof(X2), typeof(double), typeof(LineUserControl), new PropertyMetadata(0.0d, X2_PropCallback)); ;
            Y2prop = DependencyProperty.Register(nameof(Y2), typeof(double), typeof(LineUserControl), new PropertyMetadata(0.0d, Y2_PropCallback)); ;
        }

        #region public coordinates property
        public double X1
        {
            get => (double)GetValue(X1prop);
            set => SetValue(X1prop, value);
        }

        public double Y1
        {
            get => (double)GetValue(Y1prop);
            set => SetValue(Y1prop, value);
        }

        public double X2
        {
            get => (double)GetValue(X2prop);
            set => SetValue(X2prop, value);
        }

        public double Y2
        {
            get => (double)GetValue(Y2prop);
            set => SetValue(Y2prop, value);
        }
        #endregion

        #region dependency property
        public static readonly DependencyProperty X1prop;
        public static readonly DependencyProperty Y1prop;
        public static readonly DependencyProperty X2prop;
        public static readonly DependencyProperty Y2prop;
        #endregion

        #region callback

        public static void X1_PropCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs ev)
        {
            var line = depObj as LineUserControl;
            var newValue = (double)ev.NewValue;
            line.X1 = newValue;
            line.Line1.X1 = newValue;
            line.ChangeAnimationValue();
        }

        public static void Y1_PropCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs ev)
        {
            var line = depObj as LineUserControl;
            var newValue = (double)ev.NewValue;
            line.Y1 = newValue;
            line.Line1.Y1 = newValue;
            line.ChangeAnimationValue();
        }

        public static void X2_PropCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs ev)
        {
            var line = depObj as LineUserControl;
            var newValue = (double)ev.NewValue;
            line.X2 = newValue;
            line.Line1.X2 = newValue;
            line.ChangeAnimationValue();
        }

        public static void Y2_PropCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs ev)
        {
            var line = depObj as LineUserControl;
            var newValue = (double)ev.NewValue;
            line.Y2 = newValue;
            line.Line1.Y2 = newValue;
            line.ChangeAnimationValue();
        }


        #endregion

        public delegate void CompleteAnimationCallback(LineUserControl control);

        #region variables
        /// <summary>
        /// Список обработчиков события
        /// </summary>
        private readonly List<CompleteAnimationCallback> _listHandles = new List<CompleteAnimationCallback>();

        /// <summary>
        /// События на окончание анимации
        /// </summary>
        public event CompleteAnimationCallback CompleteAnimationEvent
        {
            add => _listHandles.Add(value);
            remove => _listHandles.Remove(value);
        }

        /// <summary>
        /// Скорость анимации
        /// </summary>
        public TimeSpan Speed { get; private set; }

        #endregion

        #region animations

        /// <summary>
        ///  Смена координат
        /// </summary>
        private void ChangeAnimationValue()
        {
            var timeLineCollection = Line1Anima.Storyboard.Children;
            ((DoubleAnimationUsingKeyFrames)timeLineCollection[0]).KeyFrames[0].Value = ((DoubleAnimationUsingKeyFrames)timeLineCollection[1]).KeyFrames[0].Value = ((DoubleAnimationUsingKeyFrames)timeLineCollection[0]).KeyFrames[1].Value = X1;

            ((DoubleAnimationUsingKeyFrames)timeLineCollection[2]).KeyFrames[0].Value = ((DoubleAnimationUsingKeyFrames)timeLineCollection[3]).KeyFrames[0].Value = ((DoubleAnimationUsingKeyFrames)timeLineCollection[2]).KeyFrames[1].Value = Y1;

            ((DoubleAnimationUsingKeyFrames)timeLineCollection[0]).KeyFrames[2].Value = X2;

            ((DoubleAnimationUsingKeyFrames)timeLineCollection[1]).KeyFrames[1].Value = X2;

            ((DoubleAnimationUsingKeyFrames)timeLineCollection[2]).KeyFrames[2].Value = Y2;

            ((DoubleAnimationUsingKeyFrames)timeLineCollection[3]).KeyFrames[1].Value = Y2;

        }

        /// <summary>
        /// Запуск анимации
        /// </summary>
        public void BeginAnimation()
        {
            Line1Anima.Storyboard.Begin();
        }

        /// <summary>
        /// Остановка анимации
        /// </summary>
        public void StopAnimation()
        {
            Line1Anima.Storyboard.Stop();
            Line1.X1 = X1;
            Line1.Y1 = Y1;
            Line1.X2 = X2;
            Line1.Y2 = Y2;
        }

        /// <summary>
        /// Удаление всех подписчиков
        /// </summary>
        public void RemoveAllHandles_CompleteAnimationEvent()
        {
            if (!_listHandles.Any())
                return;

            for (var i = 0; i < _listHandles.Count; i++)
            {
                CompleteAnimationEvent -= _listHandles[i];
            }
        }

        /// <summary>
        /// Установка скорости анимации
        /// </summary>
        /// <param name="time"></param>
        public void SetSpeed(TimeSpan time)
        {
            StopAnimation();
            var collection = Line1Anima.Storyboard.Children;

            ((DoubleAnimationUsingKeyFrames)collection[0]).KeyFrames[1].KeyTime
                = ((DoubleAnimationUsingKeyFrames)collection[1]).KeyFrames[1].KeyTime
                = ((DoubleAnimationUsingKeyFrames)collection[2]).KeyFrames[1].KeyTime
                = ((DoubleAnimationUsingKeyFrames)collection[3]).KeyFrames[1].KeyTime
                = new TimeSpan(0, 0, 0, 0, (int)Math.Round(time.TotalMilliseconds / 2));

            ((DoubleAnimationUsingKeyFrames)collection[0]).KeyFrames[2].KeyTime
                = ((DoubleAnimationUsingKeyFrames)collection[2]).KeyFrames[2].KeyTime = time;

            Speed = time;

        }

        /// <summary>
        /// Выполнения анимации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompleteAnimation(object sender, EventArgs e)
        {
            foreach (var callback in _listHandles)
            {
                callback(this);
            }
        }

        /// <summary>
        /// Событие загрузки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Line1Anima.Storyboard.Completed += CompleteAnimation;
        }
        #endregion


    }
}
