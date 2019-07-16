using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace HourlyBeep
{
    public class NotifyIconViewModel
    {
        public ICommand PauseCommand
        {
            get
            {
                var app = ((App)Application.Current);
                return new DelegateCommand<string>
                {
                    CanExecuteFunc = () => !app.IsPaused,
                    CommandAction = (hours) =>
                    {
                        app.Pause(int.Parse(hours));
                    }
                };
            }
        }

        public ICommand UnpauseCommand
        {
            get
            {
                var app = ((App)Application.Current);
                return new DelegateCommand
                {
                    CanExecuteFunc = () => app.IsPaused,
                    CommandAction = () =>
                    {
                        app.Unpause();
                    }
                };
            }
        }

        public ICommand TestCommand
        {
            get
            {
                return new DelegateCommand { CommandAction = () => ((App)Application.Current).Test() };
            }
        }

        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand { CommandAction = () => Application.Current.Shutdown() };
            }
        }
    }
}
