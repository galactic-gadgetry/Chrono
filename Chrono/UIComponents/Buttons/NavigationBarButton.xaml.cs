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

namespace Chrono.UIComponents.Buttons
{
    /// <summary>
    /// Interaction logic for NavigationBarButton.xaml
    /// </summary>
    public partial class NavigationBarButton : UserControl
    {
        // Dependency Properties
        public static readonly DependencyProperty ButtonClickedCommandProperty =
            DependencyProperty.Register(
                nameof(ButtonClickedCommand),
                typeof(ICommand),
                typeof(NavigationBarButton),
                new PropertyMetadata(null));

        public static readonly DependencyProperty ButtonImageSourceProperty =
            DependencyProperty.Register(
                nameof(ButtonImageSource),
                typeof(ImageSource),
                typeof(NavigationBarButton),
                new PropertyMetadata(null));

        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register(
                nameof(ButtonText),
                typeof(string),
                typeof(NavigationBarButton),
                new PropertyMetadata(string.Empty));


        /// <summary>
        /// Source for the button's image.
        /// </summary>
        public ImageSource ButtonImageSource
        {
            get => (ImageSource)GetValue(ButtonImageSourceProperty);
            set => SetValue(ButtonImageSourceProperty, value);
        }

        /// <summary>
        /// Text for the button's text.
        /// </summary>
        public string ButtonText
        {
            get => (string)GetValue(ButtonTextProperty);
            set => SetValue(ButtonTextProperty, value);
        }


        /// <summary>
        /// Executed when the button is clicked.
        /// </summary>
        public ICommand ButtonClickedCommand
        {
            get => (ICommand)GetValue(ButtonClickedCommandProperty);
            set => SetValue(ButtonClickedCommandProperty, value);
        }



        public NavigationBarButton()
        {
            InitializeComponent();
        }
    }
}
