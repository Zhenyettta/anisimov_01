using System.Windows;

namespace Anisimov01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();
            _vm = new ViewModel(OnDataInputInvalid);
            DataContext = _vm;
        }

        private void OnDataInputInvalid(object? sender, ValidateDateInput e)
        {
            MessageBox.Show(e.ErrorMsg);
        }
    }
}