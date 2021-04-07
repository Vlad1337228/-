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

namespace EVENTS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random r = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
           
            Close();
        }
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(this);        
            button1.SetValue(Canvas.LeftProperty, p.X - button1.ActualWidth / 2);
            button1.SetValue(Canvas.TopProperty, p.Y - button1.ActualHeight / 2);

           
        }
        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.Space))
                return;
            
            Canvas.SetLeft(button2, r.NextDouble() * ((Content as Canvas).ActualWidth - 5));
            Canvas.SetTop(button2, r.NextDouble() * ((Content as Canvas).ActualHeight - 5));

            if ((string)button2.Content == "Изменить")
            {
                button2.Content = "";
                button2.MouseMove += button2_MouseMove;
                button2.Click += button2_Click;
                button2.Click -= button2_Click2;
            }
        }
        
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            button2.Content = "Изменить";
            button2.MouseMove -= button2_MouseMove;
            button2.Click -= button2_Click;
            button2.Click += button2_Click2;
        }

        private void button2_Click2(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var c = Content as Canvas;
            for (int i = 0; i < 2; i++)
            {
                var b = FindName("button" + (i + 1)) as Button;
                if (Canvas.GetLeft(b) > c.ActualWidth || Canvas.GetTop(b) > c.ActualHeight)
                {
                    Canvas.SetLeft(b, 10 + i * (b.ActualWidth + 10));
                    Canvas.SetTop(b, 10);
                }
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            if (Cursor_Image.Visibility == Visibility.Hidden)
            {
                Cursor_Image.Visibility = Visibility.Visible;
                button3.Content = "Скрыть курсор";
            }
            else
            {
                Cursor_Image.Visibility = Visibility.Hidden;
                button3.Content = "Показать курсор";
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(this);         
            
            if (Cursor_Image.Visibility == Visibility.Visible)
            {
                var centreX = (Content as Canvas).ActualWidth / 2;
                var centreY = (Content as Canvas).ActualHeight / 2;
                Canvas.SetLeft(Cursor_Image, centreX + (p.X - centreX) * -1);
                Canvas.SetTop(Cursor_Image, centreY + (p.Y - centreY) * -1);
            }

        }
    }
}
