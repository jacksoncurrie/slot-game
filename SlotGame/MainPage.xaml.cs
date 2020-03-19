using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GameLogic.Entities;
using Windows.UI.Xaml.Media.Imaging;
using System.Collections.Generic;
using Windows.UI.Popups;
using System.Threading.Tasks;
using System.Linq;
using Windows.Media.SpeechSynthesis;

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
                new Spinner("Spinner1", spinnerImages, new object[] { Slot1_1, Slot2_1, Slot3_1 }),
                new Spinner("Spinner2", spinnerImages, new object[] { Slot1_2, Slot2_2, Slot3_2 }),
                new Spinner("Spinner3", spinnerImages, new object[] { Slot1_3, Slot2_3, Slot3_3 }),
                new Spinner("Spinner4", spinnerImages, new object[] { Slot1_4, Slot2_4, Slot3_4 }),
                new Spinner("Spinner5", spinnerImages, new object[] { Slot1_5, Slot2_5, Slot3_5 })
            };

            SpinnerGroup = new SpinnerGroup(spinners, spinnerImages);
        }

        private SpinnerGroup SpinnerGroup { get; set; }
        private User User { get; set; }

        private async void Spin1_Click(object sender, RoutedEventArgs e) => await DoSpin(1);
        private async void Spin10_Click(object sender, RoutedEventArgs e) => await DoSpin(10);
        private async void Spin100_Click(object sender, RoutedEventArgs e) => await DoSpin(100);

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Show instructions
            var dialog = new MessageDialog("You have $1000 to spin.\n" +
                "If you get 3 of the same horizontally, you keep your bet.\n" +
                "If you get 4 of the same horizontally, you get 4x your bet back\n" +
                "If you get 5 of the same horizontally, you get 50x your bet back\n" +
                "Good luck!", "Slot Game");
            await dialog.ShowAsync();
        }

        private async Task DoSpin(int value)
        {
            try
            {
                // Check for enough funds
                if (User.GameBalance < value)
                    throw new NotEnoughFundsException(User.GameBalance);

                SetSpinnersIsEnabled(false);

                // Animate spinners spinning
                SetSpinnersLoading(SpinnerGroup.Spinners);
                await Task.Delay(1000);

                foreach (var spinner in SpinnerGroup.Spinners)
                {
                    var resultingImages = spinner.GetRandomSpinnerImages();
                    SetImageSource(spinner.SpinnerControls[0], resultingImages[0].ImageUrl);
                    SetImageSource(spinner.SpinnerControls[1], resultingImages[1].ImageUrl);
                    SetImageSource(spinner.SpinnerControls[2], resultingImages[2].ImageUrl);
                }

                var spinnerScores = SpinnerGroup.GetSpinnersCount();
                var spinnerValues = spinnerScores.Values.ToArray();
                var winnings = SpinnerGroup.GetWinnings(spinnerValues, value);

                // Update balance
                User.GameBalance += winnings;
                SetNewBalance();
                SetSpinnersIsEnabled(true);

                // Speak result to user
                if (winnings > 0)
                    SpeakText("You won");
                else if (winnings < 0)
                    SpeakText("You lost");
            }
            catch (NotEnoughFundsException exception)
            {
                // Handle error when not enough funds are available
                var dialog = new MessageDialog(exception.Message, "Low Balance");
                await dialog.ShowAsync();
            }
        }

        private void SetSpinnersLoading(List<Spinner> spinners)
        {
            foreach (var spinner in spinners)
                foreach (var spinnerControl in spinner.SpinnerControls)
                    SetImageSource(spinnerControl, "ms-appx:///Assets/shuffling-animation.gif");
            
        }

        private void SetImageSource(object spinnerControl, string imageUrl) =>
            (spinnerControl as Image).Source = new BitmapImage(new Uri(imageUrl));

        private void SetNewBalance() =>
            Balance.Text = $"Balance: ${User.GameBalance}";

        private void SetSpinnersIsEnabled(bool isEnabled)
        {
            Spin1.IsEnabled = isEnabled;
            Spin10.IsEnabled = isEnabled;
            Spin100.IsEnabled = isEnabled;
        }

        private async void SpeakText(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                var speechSynthesizer = new SpeechSynthesizer();
                var speechStream = await speechSynthesizer.SynthesizeTextToStreamAsync(text);

                MediaElement.AutoPlay = true;
                MediaElement.SetSource(speechStream, speechStream.ContentType);
                MediaElement.Play();
            }
        }
    }
}
