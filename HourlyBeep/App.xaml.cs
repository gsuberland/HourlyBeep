using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Media;
using Hardcodet.Wpf.TaskbarNotification;
using System.Windows.Controls;

namespace HourlyBeep
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon _notifyIcon;
        private Timer _beepTimer;
        private int _currentHour = 0;
        private DateTime? _pausedUntil = null;
        private SoundPlayer _soundPlayer;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _currentHour = DateTime.Now.Hour;

            _notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            _notifyIcon.ContextMenuOpening += _notifyIcon_ContextMenuOpening;
            _notifyIcon.PreviewTrayContextMenuOpen += _notifyIcon_PreviewTrayContextMenuOpen;
            _soundPlayer = new SoundPlayer(@".\beep.wav");
            _beepTimer = new Timer(BeepTimerCallback, null, 1000, 1000);
        }

        private void _notifyIcon_PreviewTrayContextMenuOpen(object sender, RoutedEventArgs e)
        {
            foreach (Control ctl in _notifyIcon.ContextMenu.Items)
            {
                if (!(ctl is MenuItem))
                    continue;

                var mi = (MenuItem)ctl;

                if (mi.Name == @"PauseInfo")
                {
                    mi.Header = $"Paused until {_pausedUntil?.Hour ?? 0:d2}:{_pausedUntil?.Minute ?? 0:d2}";
                    mi.Visibility = _pausedUntil == null ? Visibility.Collapsed : Visibility.Visible;
                }
                else if (mi.Name == @"Unpause")
                {
                    mi.Visibility = _pausedUntil == null ? Visibility.Collapsed : Visibility.Visible;
                }
            }
        }

        private void _notifyIcon_ContextMenuOpening(object sender, System.Windows.Controls.ContextMenuEventArgs e)
        {
        }

        public bool IsPaused
        {
            get => _pausedUntil != null;
        }

        public void Test()
        {
            _soundPlayer.Play();
        }

        public void Unpause()
        {
            _pausedUntil = null;
        }

        public void Pause(int hours)
        {
            _pausedUntil = DateTime.Now.AddHours(hours);
        }

        private void BeepTimerCallback(object state)
        {
            if (_pausedUntil != null)
            {
                if (DateTime.Now < _pausedUntil)
                {
                    return;
                }
                else
                {
                    _pausedUntil = null;
                }
            }

            if (DateTime.Now.Hour != _currentHour)
            {
                _currentHour = DateTime.Now.Hour;
                _soundPlayer.Play();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon.Dispose();
            _beepTimer.Dispose();
            _soundPlayer.Dispose();
            base.OnExit(e);
        }
    }
}
