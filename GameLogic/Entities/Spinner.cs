using System;
using System.Collections.Generic;

namespace GameLogic.Entities
{
    public class Spinner
    {
        public Spinner(string spinnerName, List<SpinnerImage> spinnerImages, object[] spinnerControls)
        {
            SpinnerName = spinnerName;
            SpinnerImages = spinnerImages;
            SpinnerControls = spinnerControls;
        }

        public string SpinnerName { get; }
        public List<SpinnerImage> SpinnerImages { get; }
        public object[] SpinnerControls { get; }
        public int CurrentSpinnerLocation { get; private set; }
        public SpinnerImage CurrentSpinnerImage { get => SpinnerImages[CurrentSpinnerLocation]; }

        /// <summary>
        /// Gets a random image position
        /// </summary>
        public SpinnerImage[] GetRandomSpinnerImages()
        {
            // Get image from random number
            var randomSpin = GenerateRandomSpin(SpinnerImages.Count);
            CurrentSpinnerLocation = randomSpin;
            return GetSpinnerImages(randomSpin);
        }

        /// <summary>
        /// Moves spinner up one value
        /// </summary>
        public SpinnerImage[] BumbSpinnerUp()
        {
            var spinLocation = ++CurrentSpinnerLocation;
            return GetSpinnerImages(spinLocation);
        }

        /// <summary>
        /// Moves spinner up one value
        /// </summary>
        public SpinnerImage[] BumbSpinnerDown()
        {
            var spinLocation = --CurrentSpinnerLocation;
            return GetSpinnerImages(spinLocation);
        }

        private SpinnerImage[] GetSpinnerImages(int middleSpinnerLocation)
        {
            var topLocation = middleSpinnerLocation - 1;
            var bottomLocation = middleSpinnerLocation + 1;
            if (topLocation < 0)
                topLocation = SpinnerImages.Count - 1;
            if (bottomLocation > SpinnerImages.Count - 1)
                bottomLocation = 0;

            return new SpinnerImage[]
            {
                SpinnerImages[topLocation],
                SpinnerImages[middleSpinnerLocation],
                SpinnerImages[bottomLocation]
            };
        }

        private int GenerateRandomSpin(int numberOfItems)
        {
            // Get random number up to the number of spinner images
            var random = new Random();
            return random.Next(numberOfItems);
        }
    }
}
