using System.Collections.Generic;
namespace GameLogic.Entities
{
    class SpinnerGroup
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
    }
}
