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
using System.Windows.Threading;

namespace Ballspiel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer animationTimer = new DispatcherTimer();
        bool nachRechts = true;
        bool nachUnten = true;
        private int zaehler = 0;

        public MainWindow()
        {
            InitializeComponent();

            animationTimer.Interval = TimeSpan.FromMilliseconds(20);
            animationTimer.Tick += PositioniereBall;
        }

        private void PositioniereBall(object sender, EventArgs e)
        {
            var x = Canvas.GetLeft(Ball);
            if (nachRechts)
            {
                Canvas.SetLeft(Ball, x + 3);
            }
            else
            {
                Canvas.SetLeft(Ball, x - 3);
            }

            if (x >= Spielfeld.ActualWidth - Ball.ActualWidth) nachRechts = false;
            if (x <= 0) nachRechts = true;

            var y = Canvas.GetTop(Ball);
            if (nachUnten)
            {
                Canvas.SetTop(Ball, y + 3);
            }
            else
            {
                Canvas.SetTop(Ball, y - 3);
            }
            if (y >= Spielfeld.ActualHeight - Ball.ActualWidth) nachUnten = false;
            if (y <= 0) nachUnten = true;

        }

        private void cmdStartStop_Click(object sender, RoutedEventArgs e)
        {
            if (animationTimer.IsEnabled)
            {
                animationTimer.Stop();
            }
            else
            {
                animationTimer.Start();
                zaehler = 0;
                lblSpielstand.Content = $"{zaehler} Clicks";
            }


        }

        private void Ball_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (animationTimer.IsEnabled)
            {
                zaehler++;
                lblSpielstand.Content = $"{zaehler} Clicks";
            }
        }

        private void Ball_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F)
            {
                Random rand = new Random();
                Ball.Fill = new SolidColorBrush(Color.FromRgb((byte)rand.Next(0, 256), (byte)rand.Next(0, 256), (byte)rand.Next(0, 256)));

                //Ball.Fill = Brushes.Red;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down && !nachUnten) nachUnten = true;
            if (e.Key == Key.Up && nachUnten) nachUnten = false;

            if (e.Key == Key.Left && nachRechts) nachRechts = false;
            if (e.Key == Key.Right && !nachRechts) nachRechts = true;
        }
    }
}
