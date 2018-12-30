using System;
using System.Drawing;
using System.Drawing.Imaging;
using ImageEditor.Models;

namespace ImageEditor
{
    public abstract class ImageEditor
    {
        protected Bitmap _sourceImage;
        protected Bitmap _targetImage;

        public ImageEditor(string sourceFileName)
        {
            _sourceImage = Image.FromFile(sourceFileName) as Bitmap;
        }

        public void SaveTargetImage(string targetFileName)
        {
            _targetImage.Save(targetFileName, ImageFormat.Png);
            Console.WriteLine($"{targetFileName} is saved successfully");
        }

        public virtual void AddIcon(IconInput input)
        {
            if (input.XCoordinate >= _targetImage.Width || input.YCoordinate >= _targetImage.Height)
            {
                throw new ArgumentException("Image's area must contain Icons coordinates");
            }

            using(Graphics gg = Graphics.FromImage(_targetImage))
            {
                gg.DrawIcon(input.Icon, input.XCoordinate, input.YCoordinate);
            }
        }

        public abstract void CropImage(CropInput cropInput);
    }
}