using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GameLogic.Entities;
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

            // Set up application
            User = new User
            {
                GameBalance = 300
            };
        }

        private User User { get; set; }

        private void Spin1_Click(object sender, RoutedEventArgs e) => DoSpin(1);
        private void Spin10_Click(object sender, RoutedEventArgs e) => DoSpin(10);
        private void Spin100_Click(object sender, RoutedEventArgs e) => DoSpin(100);

        private int DoSpin(int value)
        {
            if (User.GameBalance <= 0)
                
            User.GameBalance -= value;
            Slot1.Source = new BitmapImage(new Uri("ms-appx:///Assets/spades.png"));
            return value;
        }
    }
}
