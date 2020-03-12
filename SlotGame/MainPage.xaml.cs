using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GameLogic.Entities;
using Windows.UI.Xaml.Media.Imaging;
using System.Collections.Generic;
using Windows.UI.Popups;
using System.Threading.Tasks;

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

            User = new User(300);

            var spinnerImages = new List<SpinnerImage>
            {
                new SpinnerImage("Hearts", "ms-appx:///Assets/hearts.png", 1),
                new SpinnerImage("Spades", "ms-appx:///Assets/spades.png", 1),
                new SpinnerImage("Diamonds", "ms-appx:///Assets/diamonds.png", 1),
                new SpinnerImage("Clubs", "ms-appx:///Assets/clubs.png", 1)
            };

            var spinners = new List<Spinner>
            {
                new Spinner("Spinner1", spinnerImages),
                new Spinner("Spinner2", spinnerImages),
                new Spinner("Spinner3", spinnerImages),
                new Spinner("Spinner4", spinnerImages),
                new Spinner("Spinner5", spinnerImages)
            };

            SpinnerGroup = new SpinnerGroup(spinners, spinnerImages);
        }

        private SpinnerGroup SpinnerGroup { get; set; }
        private User User { get; set; }

        private async void Spin1_Click(object sender, RoutedEventArgs e) => await DoSpin(1);
        private async void Spin10_Click(object sender, RoutedEventArgs e) => await DoSpin(10);
        private async void Spin100_Click(object sender, RoutedEventArgs e) => await DoSpin(100);

        private async Task DoSpin(int value)
        {
            if (User.GameBalance < value)
            {
                var dialog = new MessageDialog("Not enough funds to spin.", "Low Balance");
                await dialog.ShowAsync();
                return;
            }
                
            User.GameBalance -= value;

            var currentSpinner = 0;
            foreach (var spinner in SpinnerGroup.Spinners)
            {
                currentSpinner++;
                var resultingImages = spinner.GetRandomSpinnerImages();
                Slot1_1.Source = new BitmapImage(new Uri(resultingImages[0].ImageUrl));
                Slot2_1.Source = new BitmapImage(new Uri(resultingImages[1].ImageUrl));
                Slot3_1.Source = new BitmapImage(new Uri(resultingImages[2].ImageUrl));
            }
        }
    }
}
