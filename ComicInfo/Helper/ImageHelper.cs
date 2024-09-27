using SkiaSharp;
using System.Drawing;

namespace Chris82111.ComicInfo.Helper
{
    public class ImageHelper
    {
        /// <summary>
        ///         Fast way to read the width and height of a picture
        /// <br/>   
        /// <br/>   <see href="https://gist.github.com/dejanstojanovic/c5df7310174b570c16bc"/>
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public static Size PictureSize(string imagePath)
        {
            using (var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var image = SKImage.FromEncodedData(fileStream))
                {
                    return new Size(image.Width, image.Height);
                }
            }
        }
    }
}
