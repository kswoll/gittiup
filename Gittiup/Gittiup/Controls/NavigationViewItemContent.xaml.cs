using Windows.UI.Xaml;
using FontAwesome.UWP;

namespace Gittiup.Controls
{
    public sealed partial class NavigationViewItemContent
    {
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof (Icon), typeof(FontAwesomeIcon), typeof (FontAwesome.UWP.FontAwesome), new PropertyMetadata((object) FontAwesomeIcon.None));

        public NavigationViewItemContent()
        {
            this.InitializeComponent();
        }

        /// <summary>Gets or sets the FontAwesome icon</summary>
        public FontAwesomeIcon Icon
        {
            get => (FontAwesomeIcon)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
    }
}
