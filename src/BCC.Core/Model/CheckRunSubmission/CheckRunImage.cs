namespace BCC.Core.Model.CheckRunSubmission
{
    public class CheckRunImage
    {
        public CheckRunImage(string alt, string imageUrl)
        {
            Alt = alt;
            ImageUrl = imageUrl;
        }

        public string Alt { get; set; }

        public string ImageUrl { get; set; }

        public string Caption { get; set; }
    }
}