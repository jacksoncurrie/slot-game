using System.Collections.Generic;
using System.Linq;

namespace GameLogic.Entities
{
    public class SpinnerGroup
    {
        public SpinnerGroup(List<Spinner> spinners, List<SpinnerImage> allSpinnerImages)
        {
            Spinners = spinners;
            AllSpinnerImages = allSpinnerImages;
        }

        public List<Spinner> Spinners { get; set; }
        public List<SpinnerImage> AllSpinnerImages { get; }

        /// <summary>
        /// Gets the number of wheels with the same image
        /// </summary>
        public Dictionary<SpinnerImage, int> GetSpinnersCount()
        {
            // Initalise scores with zero
            var spinnerScores = new Dictionary<SpinnerImage, int>();
            AllSpinnerImages.ForEach(spinnerImage => spinnerScores.Add(spinnerImage, 0));
            // Count the spinners images and increment score
            Spinners.ForEach(spinner => spinnerScores[spinner.CurrentSpinnerImage]++);
            return spinnerScores;
        }

        public int GetWinnings(int[] spinnerValues, int spinBid)
        {
            int highestScore = spinnerValues.Max();
            int winnings = -spinBid;

            // Winning result multiplier 
            switch (highestScore)
            {
                case 3:
                    winnings = 0;
                    break;

                case 4:
                    winnings = spinBid * 4;
                    break;

                case 5:
                    winnings = spinBid * 50;
                    break;
            }

            return winnings;
        }
    }
}
