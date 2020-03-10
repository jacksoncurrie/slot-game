using System;
using System.Collections.Generic;

namespace GameLogic.Entities
{
    public class Spinner
    {
        public Spinner(List<SpinnerImage> spinnerImages)
        {
            SpinnerImages = spinnerImages;
        }

        public List<SpinnerImage> SpinnerImages { get; }
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
            return new SpinnerImage[]
            {
                SpinnerImages[middleSpinnerLocation - 1],
                SpinnerImages[middleSpinnerLocation],
                SpinnerImages[middleSpinnerLocation + 1]
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
