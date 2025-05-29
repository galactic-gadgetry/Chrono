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

namespace Chrono.UIComponents.Cards
{
    /// <summary>
    /// Interaction logic for ProjectListCard.xaml
    /// </summary>
    public partial class ProjectListCard : UserControl
    {
        // Dependency Properties
        public static readonly DependencyProperty CodeTextProperty =
            DependencyProperty.Register(
                nameof(CodeText),
                typeof(string),
                typeof(ProjectListCard),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty DeleteButtonClickedCommandParameterProperty =
            DependencyProperty.Register(
                nameof(DeleteButtonClickedCommandParameter),
                typeof(object),
                typeof(ProjectListCard),
                new PropertyMetadata(null));

        public static readonly DependencyProperty DeleteButtonClickedCommandProperty =
            DependencyProperty.Register(
                nameof(DeleteButtonClickedCommand),
                typeof(ICommand),
                typeof(ProjectListCard),
                new PropertyMetadata(null));

        public static readonly DependencyProperty EditButtonClickedCommandParameterProperty =
            DependencyProperty.Register(
                nameof(EditButtonClickedCommandParameter),
                typeof(object),
                typeof(ProjectListCard),
                new PropertyMetadata(null));

        public static readonly DependencyProperty EditButtonClickedCommandProperty =
            DependencyProperty.Register(
                nameof(EditButtonClickedCommand),
                typeof(ICommand),
                typeof(ProjectListCard),
                new PropertyMetadata(null));

        public static readonly DependencyProperty NameTextProperty =
            DependencyProperty.Register(
                nameof(NameText),
                typeof(string),
                typeof(ProjectListCard),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty StatusTextProperty =
            DependencyProperty.Register(
                nameof(StatusText),
                typeof(string),
                typeof(ProjectListCard),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty WbsTextProperty =
            DependencyProperty.Register(
                nameof(WbsText),
                typeof(string),
                typeof(ProjectListCard),
                new PropertyMetadata(string.Empty));


        /// <summary>
        /// Text for the Code label.
        /// </summary>
        public string CodeText
        {
            get => (string)GetValue(CodeTextProperty);
            set => SetValue(CodeTextProperty, value);
        }

        /// <summary>
        /// Object passed as a parameter when the Delete button is
        /// clicked.
        /// </summary>
        public object DeleteButtonClickedCommandParameter
        {
            get => (object)GetValue(DeleteButtonClickedCommandParameterProperty);
            set => SetValue(DeleteButtonClickedCommandParameterProperty, value);
        }

        /// <summary>
        /// Object passed as a parameter when the Edit button is
        /// clicked.
        /// </summary>
        public object EditButtonClickedCommandParameter
        {
            get => (object)GetValue(EditButtonClickedCommandParameterProperty);
            set => SetValue(EditButtonClickedCommandParameterProperty, value);
        }

        /// <summary>
        /// Text for the Name label.
        /// </summary>
        public string NameText
        {
            get => (string)GetValue(NameTextProperty);
            set => SetValue(NameTextProperty, value);
        }

        /// <summary>
        /// Text for the status label.
        /// </summary>
        public string StatusText
        {
            get => (string)GetValue(StatusTextProperty);
            set => SetValue(StatusTextProperty, value);
        }

        /// <summary>
        /// Text for the WBS label.
        /// </summary>
        public string WbsText
        {
            get => (string)GetValue(WbsTextProperty);
            set => SetValue(WbsTextProperty, value);
        }


        /// <summary>
        /// Executed when the Delete button is clicked.
        /// </summary>
        public ICommand DeleteButtonClickedCommand
        {
            get => (ICommand)GetValue(DeleteButtonClickedCommandProperty);
            set => SetValue(DeleteButtonClickedCommandProperty, value);
        }

        /// <summary>
        /// Executed when the Edit button is clicked.
        /// </summary>
        public ICommand EditButtonClickedCommand
        {
            get => (ICommand)GetValue(EditButtonClickedCommandProperty);
            set => SetValue(EditButtonClickedCommandProperty, value);
        }



        public ProjectListCard()
        {
            InitializeComponent();
        }
    }
}
