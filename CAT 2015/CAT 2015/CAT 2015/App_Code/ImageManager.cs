using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Configuration;
using System.IO;

namespace CAT_2015.AppCode
{
    public static class ImageManager // Added 13/01/16
    {
        /// <summary>
        /// Takes the url of an image and turns that image into a greyscale variant. Returns the new
        /// image. If the image already exists, just sends the url of the image.
        /// </summary>
        public static string GetGreyScale(string url)
        {
            // pull setting from Web.config file
            string greyscaleSuffix = ConfigurationManager.AppSettings["greyscaleSuffix"];

            // If the url is something, and not a null value or empty string...
            if (url != null && url != "")
            {
                // Get the greyscale url by appending the greyscale suffix.
                string greyScaleUrl = appendSuffixToFile(url, greyscaleSuffix);
                // If the image does not already exist...
                if (!imageExists(greyScaleUrl))
                {
                    // Load the normal variant
                    Bitmap normalImage = (Bitmap)loadImage(url);
                    // Create a greyscale variant from it
                    Bitmap greyScaleImage = makeGrayscale3(normalImage);
                    // Save the greyscale variant
                    saveImage(greyScaleImage, greyScaleUrl);
                }
                // If the image already exists, or once it has been created, return it.
                return greyScaleUrl;
            }
            // If something goes wrong, return the url of the original image.
            return url;
        }

        // Added 13/01/16
        // Credit: http://stackoverflow.com/questions/2265910/convert-an-image-to-grayscale
        private static Bitmap makeGrayscale3(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
            new float[][] 
            {
                new float[] {.3f, .3f, .3f, 0, 0},
                new float[] {.59f, .59f, .59f, 0, 0},
                new float[] {.11f, .11f, .11f, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
            });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }

        /// <summary>
        /// Takes a string, seperates the file type from the rest of it, and returns both of them
        /// as the prefix and suffix
        /// </summary>
        private static void getType(string name, out string prefix, out string suffix)
        {
            // Split the string by the '.' character
            string[] strings = name.Split(new char[] { '.' });
            prefix = "";
            suffix = "";
            // For each string...
            for (int i = 0; i < strings.Length; i++)
            {
                if (i == strings.Length - 1) // If it is the last string...
                {
                    // Make that strin the suffix
                    suffix = strings[i];
                }
                else // Otherwise...
                {
                    // Add the string onto the prefix. This allows filenames with '.' in them.
                    prefix += strings[i];
                }
            }
        }

        /// <summary>
        /// Checks to see if the image already exists. Returns true if it does.
        /// </summary>
        private static bool imageExists(string url)
        {
            string baseDir = ConfigurationManager.AppSettings["websiteBaseDirectory"];
            string imageDir = baseDir + url.Remove(0, 1);
            if (File.Exists(imageDir))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Loads the image from a imagefolder url into the program as an image object.
        /// </summary>
        private static Image loadImage(string url)
        {
            Bitmap image = null;
            string baseDir = ConfigurationManager.AppSettings["websiteBaseDirectory"];
            string imageDir = baseDir + url.Remove(0, 1);
            if (File.Exists(imageDir))
            {
                try
                {
                    image = (Bitmap)Bitmap.FromFile(imageDir);
                }
                catch
                {
                    Console.WriteLine("Could not load image " + imageDir);
                    return null;
                }
            }
            return image;
        }

        /// <summary>
        /// Saves the image in the correct place in the image folder.
        /// </summary>
        private static void saveImage(Image image, string url)
        {
            string baseDir = ConfigurationManager.AppSettings["websiteBaseDirectory"];
            string imageDir = baseDir + url.Remove(0, 1);
            image.Save(imageDir);
        }

        /// <summary>
        /// Appends a defined suffix to the file before the type.
        /// </summary>
        private static string appendSuffixToFile(string file, string suffix)
        {
            string imageUrl;
            string type;
            getType(file, out imageUrl, out type);
            return imageUrl + suffix + "." + type;
        }
    }
}