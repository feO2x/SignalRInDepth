using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SignalRWpfClient
{
    // ReSharper disable once RedundantExtendsListEntry
    public partial class MainWindow : Window
    {
        public IMainWindowViewModel ViewModel
        {
            get { return (IMainWindowViewModel) DataContext; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                DataContext = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            TextBox.AddHandler(PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(SelectivelyIgnoreMouseButton), true);
            TextBox.AddHandler(GotKeyboardFocusEvent, new RoutedEventHandler(SelectAllText), true);
            TextBox.AddHandler(MouseDoubleClickEvent, new RoutedEventHandler(SelectAllText), true);
        }

        private static void SelectivelyIgnoreMouseButton(object sender,
                                                         MouseButtonEventArgs e)
        {
            // Find the TextBox
            DependencyObject parent = e.OriginalSource as UIElement;
            while (parent != null && !(parent is TextBox))
                parent = VisualTreeHelper.GetParent(parent);

            if (parent == null) return;

            var textBox = (TextBox) parent;
            if (textBox.IsKeyboardFocusWithin) return;

            // If the text box is not yet focussed, give it the focus and
            // stop further processing of this click event.
            textBox.Focus();
            e.Handled = true;
        }

        private static void SelectAllText(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            if (textBox != null)
                textBox.SelectAll();
        }

        private void SendMessageOnTextBoxEnter(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;

            ViewModel.SendMessageCommand.Execute(null);
            TextBox.SelectAll();
        }
    }
}