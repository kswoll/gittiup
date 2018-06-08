using Windows.UI.Xaml;
using FontAwesome.UWP;

namespace Gittiup.Controls
{
    public sealed partial class NavigationViewItemContent
    {
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(FontAwesomeIcon), typeof(NavigationViewItemContent), new PropertyMetadata(FontAwesomeIcon.None));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(NavigationViewItemContent), new PropertyMetadata(null));

        public NavigationViewItemContent()
        {
            InitializeComponent();
        }

        public FontAwesomeIcon Icon
        {
            get => (FontAwesomeIcon)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
    }
}
