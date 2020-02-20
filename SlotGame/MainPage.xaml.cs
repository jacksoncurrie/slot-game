using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SlotGame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Spin1_Click(object sender, RoutedEventArgs e)
        {
            Slot1.Source = new BitmapImage(new Uri("ms-appx:///Assets/Cross.png"));
        }

        private void Spin10_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Spin100_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
