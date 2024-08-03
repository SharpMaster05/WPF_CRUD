using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows;

namespace WPF_CRUD.Infrastucture;

internal class Animation
{
    public static void CloseAnimation(Border border, string windowTitle)
    {
        var width = (int)border.ActualWidth;
        var time = TimeSpan.FromSeconds(0.7);

        DoubleAnimation animation =
            new(width, 0, time)
            {
                EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 }
            };

        animation.Completed += (obj, e) =>
        {
            foreach (Window i in App.Current.Windows)
            {
                if (i.Title == windowTitle)
                    i.Close();
            }
        };

        border.BeginAnimation(FrameworkElement.WidthProperty, animation);
    }

    public static void Maximize(Border border)
    {
        var view = App.Current.MainWindow;

        if (view.WindowState == WindowState.Maximized)
            MaximizeAnimation(border, view, WindowState.Normal);
        else
            MaximizeAnimation(border, view, WindowState.Maximized);
    }

    public static void Minimize(Border border)
    {
        var view = App.Current.MainWindow;
        var time = TimeSpan.FromSeconds(0.3);

        var actualHeight = (int)view.ActualHeight;
        var actualWidth = (int)view.ActualWidth;

        DoubleAnimation hideHeight = new DoubleAnimation(actualHeight, 0, time)
        {
            EasingFunction = new PowerEase() { Power = 2, EasingMode = EasingMode.EaseOut },
            FillBehavior = FillBehavior.Stop
        };
        DoubleAnimation hideWidth = new DoubleAnimation(actualWidth, 0, time)
        {
            EasingFunction = new PowerEase() { Power = 2, EasingMode = EasingMode.EaseOut },
            FillBehavior = FillBehavior.Stop
        };

        hideHeight.Completed += (s, e) => view.WindowState = WindowState.Minimized;

        border.BeginAnimation(FrameworkElement.HeightProperty, hideHeight);
        border.BeginAnimation(FrameworkElement.WidthProperty, hideWidth);
    }

    private static void MaximizeAnimation(Border border, Window window, WindowState state)
    {
        var time = TimeSpan.FromSeconds(0.4);

        var actualHeight = (int)window.ActualHeight;

        DoubleAnimation hideHeight = new DoubleAnimation(actualHeight, 0, time)
        {
            EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 3 },
        };

        hideHeight.Completed += (s, e) =>
        {
            window.WindowState = state;

            var targetHeight = (int)window.ActualHeight;

            DoubleAnimation fadeHeight = new DoubleAnimation(0, targetHeight, time)
            {
                EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 3 },
                FillBehavior = FillBehavior.Stop
            };
            border.BeginAnimation(FrameworkElement.HeightProperty, fadeHeight);
        };
        border.BeginAnimation(FrameworkElement.HeightProperty, hideHeight);
    }

    public static void ChangePageAnimation(Frame frame, Page page)
    {
        var time = TimeSpan.FromSeconds(0.3);
        DoubleAnimation hide =
            new(1, 0, time)
            {
                EasingFunction = new PowerEase() { Power = 2, EasingMode = EasingMode.EaseOut }
            };

        hide.Completed += (s, e) =>
        {
            frame.Content = page;

            DoubleAnimation fade =
                new(0, 1, time)
                {
                    EasingFunction = new PowerEase { Power = 2, EasingMode = EasingMode.EaseIn }
                };

            frame.BeginAnimation(UIElement.OpacityProperty, fade);
        };

        frame.BeginAnimation(UIElement.OpacityProperty, hide);
    }

    public delegate void ChangeDisplay();
    public static void ChangeDisplayAnimation(ScrollViewer scroller, ChangeDisplay change)
    {
        var time = TimeSpan.FromSeconds(0.3);

        DoubleAnimation hide = new(1, 0, time);

        hide.Completed += (s, e) =>
        {
            change();

            DoubleAnimation fade = new(0, 1, time);

            scroller.BeginAnimation(UIElement.OpacityProperty, fade);
        };

        scroller.BeginAnimation(UIElement.OpacityProperty, hide);
    }
}
