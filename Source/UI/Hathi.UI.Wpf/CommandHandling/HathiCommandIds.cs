using System.Windows.Input;

namespace Hathi.UI.Wpf.CommandHandling
{
    public static class HathiCommandIds
    {
        public static readonly RoutedCommand Servers =
            new RoutedCommand("ImportGedcom", typeof(HathiCommandTarget));

        public static readonly RoutedCommand Friends =
           new RoutedCommand("ImportGedcom", typeof(HathiCommandTarget));

        public static readonly RoutedCommand Download =
           new RoutedCommand("ImportGedcom", typeof(HathiCommandTarget));

        public static readonly RoutedCommand Upload =
           new RoutedCommand("ImportGedcom", typeof(HathiCommandTarget));

        public static readonly RoutedCommand Search =
           new RoutedCommand("ImportGedcom", typeof(HathiCommandTarget));

        public static readonly RoutedCommand Options =
           new RoutedCommand("ImportGedcom", typeof(HathiCommandTarget));
    }
}
