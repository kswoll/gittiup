using Movel.Utils;

namespace Movel.Wpf
{
    public class MovelApp
    {
        public static void Initialize()
        {
            MovelDispatcher.Instance = new WpfMovelDispatcher();
        }
    }
}