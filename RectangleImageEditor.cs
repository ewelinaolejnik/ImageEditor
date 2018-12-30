using System;
using System.Drawing;
using ImageEditor.Models;

namespace ImageEditor
{
    public class RectangleImageEditor : ImageEditor
    {
        public RectangleImageEditor(string sourceFileName) : base(sourceFileName) { }

        public override void CropImage(CropInput cropInput)
        {
            var input = cropInput as RectangleCropInput;
            if (input.DecreasePercentage >= 100)
            {
                throw new ArgumentException("Value of percentage to decrease image must be less than 100%");
            }

            float fraction = input.DecreasePercentage / 100.0f;
            Rectangle cropRectangle = new Rectangle(
                (int) (_sourceImage.Width * (fraction / 2)),
                (int) (_sourceImage.Height * (fraction / 2)),
                (int) (_sourceImage.Width * 3 * fraction),
                (int) (_sourceImage.Height * 3 * fraction));

            _targetImage = new Bitmap(cropRectangle.Width, cropRectangle.Height);

            using(Graphics graphics = Graphics.FromImage(_targetImage))
            {
                graphics.DrawImage(_sourceImage, new Rectangle(0, 0, _targetImage.Width, _targetImage.Height),
                    cropRectangle,
                    GraphicsUnit.Pixel);
            }
        }
    }
}