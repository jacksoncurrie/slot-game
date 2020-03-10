namespace GameLogic.Entities
{
    public class SpinnerImage
    {
        public SpinnerImage(string spinnerName, string imageUrl, int imageValue)
        {
            SpinnerName = spinnerName;
            ImageUrl = imageUrl;
            ImageValue = imageValue;
        }

        public string SpinnerName { get; set; }
        public string ImageUrl { get; set; }
        public int ImageValue { get; set; }
    }
}
