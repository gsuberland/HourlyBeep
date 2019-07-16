# Hourly Beep
Simple little .NET WPF app that beeps on the hour like a digital watch.

Appears as a tray icon. Context menu allows pausing of the beeps for a number of hours.

Any sound can be used simply by replacing "beep.wav" in the application directory.

## Code

This application utilises the [Hardcodet WPF NotifyIcon](https://github.com/hardcodet/wpf-notifyicon) NuGet package for running the tray icon, as WPF has no native support for tray icons. As usual, WPF makes things harder than necessary.

Most of the application code is in App.xaml.cs as there is no application window.

Tray commands are defined in NotifyIconResources.xaml as bindings. These point to command definitions in NotifyIconViewModel.cs, each of which specifies a condition by which to enable or disable the command, plus a command action. The command actions then call into the App instance (code in App.xaml.cs) in order to run whatever functionality is needed.

## License

This software is licensed under the MIT License.

## Maintenance / Pull Requests

I wrote this because I'm ADD as hell and it helps me keep track of how long I've been working on stuff. As such there's no guarantee I'll maintain it. It's simple enough so it probably won't break, but no guarantees.

Pull requests are welcome. Try to stick to the code style I'm using but it's not a big deal.
