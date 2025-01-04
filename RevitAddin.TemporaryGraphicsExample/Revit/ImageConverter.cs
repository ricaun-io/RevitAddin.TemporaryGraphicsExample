using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace RevitAddin.TemporaryGraphicsExample.Revit
{
    public class ImageConverter
    {
        public static void ConvertPngToBmp(string inputPngPath, string outputBmpPath)
        {
            try
            {
                // Load the PNG image from the input path
                using (Image image = Image.FromFile(inputPngPath))
                {
                    // Save the image as a BMP to the output path
                    image.Save(outputBmpPath, ImageFormat.Bmp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during conversion: " + ex.Message);
            }
        }

        public static void ConvertPngToBmpWithBackground(string inputPngPath, string outputBmpPath, Color backgroundColor)
        {
            try
            {
                // Load the PNG image from the input path
                using (Image originalImage = Image.FromFile(inputPngPath))
                {
                    // Create a new bitmap with the same dimensions as the original image
                    using (Bitmap newImage = new Bitmap(originalImage.Width, originalImage.Height))
                    {
                        // Create a Graphics object to draw on the new bitmap
                        using (Graphics graphics = Graphics.FromImage(newImage))
                        {
                            // Fill the background with the specified background color
                            graphics.Clear(backgroundColor);

                            // Draw the original image on top of the background
                            graphics.DrawImage(originalImage, 0, 0);
                        }

                        // Save the new image as a BMP file
                        newImage.Save(outputBmpPath, ImageFormat.Bmp);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during conversion: " + ex.Message);
            }
        }
    }
}
