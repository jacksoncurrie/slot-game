using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GameLogic.Entities;
using Windows.UI.Xaml.Media.Imaging;
using System.Collections.Generic;
using Windows.UI.Popups;
using System.Threading.Tasks;
using System.Linq;

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

            User = new User(1000);

            var spinnerImages = new List<SpinnerImage>
            {
                new SpinnerImage("Hearts", "ms-appx:///Assets/hearts.png", 1),
                new SpinnerImage("Spades", "ms-appx:///Assets/spades.png", 1),
                new SpinnerImage("Diamonds", "ms-appx:///Assets/diamonds.png", 1),
                new SpinnerImage("Clubs", "ms-appx:///Assets/clubs.png", 1)
            };

            var spinners = new List<Spinner>
            {
                new Spinner("Spinner1", spinnerImages, new object[] {
                    Slot1_1, Slot2_1, Slot3_1
                }),
                new Spinner("Spinner2", spinnerImages, new object[] 
                {
                    Slot1_2, Slot2_2, Slot3_2
                }),
                new Spinner("Spinner3", spinnerImages, new object[] {
                    Slot1_3, Slot2_3, Slot3_3
                }),
                new Spinner("Spinner4", spinnerImages, new object[] {
                    Slot1_4, Slot2_4, Slot3_4
                }),
                new Spinner("Spinner5", spinnerImages, new object[] {
                    Slot1_5, Slot2_5, Slot3_5
                })
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

            var currentSpinner = 0;
            foreach (var spinner in SpinnerGroup.Spinners)
            {
                currentSpinner++;
                var resultingImages = spinner.GetRandomSpinnerImages();
                SetImageSource(spinner.SpinnerControls[0], resultingImages[0].ImageUrl);
                SetImageSource(spinner.SpinnerControls[1], resultingImages[1].ImageUrl);
                SetImageSource(spinner.SpinnerControls[2], resultingImages[2].ImageUrl);
            }

            var spinnerScores = SpinnerGroup.GetSpinnersCount();
            int highestScore = spinnerScores.Values.Max();
            int winnings = -value;
            switch (highestScore)
            {
                case 3:
                    winnings = 0;
                    break;

                case 4:
                    winnings = value * 5;
                    break;

                case 5:
                    winnings = value * 100;
                    break;
            }

            User.GameBalance += winnings;
            SetNewBalance();
        }

        private void SetImageSource(object spinnerControl, string imageUrl) =>
            (spinnerControl as Image).Source = new BitmapImage(new Uri(imageUrl));

        private void SetNewBalance() =>
            Balance.Text = $"Balance: ${User.GameBalance}";
    }
}
