using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PurpleRain
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<RainDrop> rainDropsList = new List<RainDrop>();
        private DispatcherTimer timer = new DispatcherTimer();

        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            Setup();
        }

        public void Setup()
        {
            for (int i = 0; i < 250; i++)
            {
                SpawnRainDrop(true);
            }

            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += Timer_tick;
            timer.Start();
        }

        private void Timer_tick(object sender, EventArgs e)
        {
            foreach (var rainDrop in rainDropsList)
            {
                Shape rectangle = rainDrop.GetRectangle();
                double top = Canvas.GetTop(rectangle);

                if (top > canvas.ActualHeight)
                {
                    //We are reusing same raindrop to prevent performance drop
                    Canvas.SetLeft(rectangle, GetRandomX());
                    Canvas.SetTop(rectangle, 0);
                    continue;
                }

                Canvas.SetTop(rectangle, top + rainDrop.GetFallSpeed());
            }
        }

        private void SpawnRainDrop(bool freshStart)
        {
            RainDrop rainDrop = new RainDrop();
            Shape rectangle = rainDrop.GetRectangle();
            Canvas.SetLeft(rectangle, GetRandomX());
            if(freshStart)
            {
                Canvas.SetTop(rectangle, GetRandomY());
            }
            else
            {
                Canvas.SetTop(rectangle, 0);
            }
            canvas.Children.Add(rectangle);
            rainDropsList.Add(rainDrop);
        }

        private double GetRandomX()
        {
            return random.NextDouble() * mainWindow.Width;
        }
        
        private double GetRandomY()
        {
            return random.NextDouble() * mainWindow.Height;
        }

        //Extra method that stops rain on mouse click
        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.timer.IsEnabled)
                this.timer.Stop();
            else
                this.timer.Start();
        }
    }
}
