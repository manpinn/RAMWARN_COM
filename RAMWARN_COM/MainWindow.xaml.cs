using RAMWARN_COM.Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RAMWARN_COM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += async (s, e) => await UpdateRamAsync();
            timer.Start();
        }

        private async Task UpdateRamAsync()
        {
            await Task.Run(async () =>
            {
                try
                {
                    await RAMReadService.ReadRAM(RamTextBlock);
                }
                catch (OutOfMemoryException)
                {
                    MessageBox.Show("Out of memory exception occurred while writing to RAM.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(async () =>
            {
                try
                {
                    await RAMWriteService.WriteToRAM();
                }
                catch (OutOfMemoryException)
                {
                    MessageBox.Show("Out of memory exception occurred while writing to RAM.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }); 
        }
    }
}