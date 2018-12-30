using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using ImageEditor.Models;

namespace ImageEditor
{
    public class EllipseImageEditor : ImageEditor
    {
        public EllipseImageEditor(string sourceFileName) : base(sourceFileName) { }

        public override void CropImage(CropInput cropInput)
        {
            var input = cropInput as EllipseCropInput;
            if (_sourceImage.Height <= input.HeightOffset || _sourceImage.Width <= input.WidthOffset)
            {
                throw new ArgumentException("Offset cannot be greater than width or height");
            }

            _targetImage = new Bitmap(_sourceImage.Width, _sourceImage.Height);
            var graphicsPath = new GraphicsPath();
            Rectangle cropRectangle = new Rectangle(input.WidthOffset / 2, input.HeightOffset / 2,
                _sourceImage.Width - input.WidthOffset, _sourceImage.Height - input.HeightOffset);
            graphicsPath.AddEllipse(cropRectangle);

            using(Graphics graphics = Graphics.FromImage(_targetImage))
            {
                graphics.SetClip(graphicsPath);
                graphics.Clear(Color.White);
                graphics.DrawImage(_sourceImage, new Rectangle(0, 0, _sourceImage.Width, _sourceImage.Height),
                    0, 0, _sourceImage.Width, _sourceImage.Height, GraphicsUnit.Pixel);
            }

            _targetImage.MakeTransparent(Color.White);
        }

        public override void AddIcon(IconInput input)
        {
            Color upperLeftPixel = _targetImage.GetPixel(input.XCoordinate, input.YCoordinate);
            Color upperRightPixel = _targetImage.GetPixel(input.XCoordinate + input.Icon.Width, input.YCoordinate);
            Color bottomLeftPixel = _targetImage.GetPixel(input.XCoordinate, input.YCoordinate - input.Icon.Height);
            Color bottomRightPixel = _targetImage.GetPixel(input.XCoordinate + input.Icon.Width, input.YCoordinate - input.Icon.Height);
            
            if (upperLeftPixel.A == 0 || upperRightPixel.A == 0 || bottomLeftPixel.A == 0 || bottomRightPixel.A == 0)
            {
                throw new ArgumentException("You cannot add icon at transparent place! Please, try again.");
            }

            base.AddIcon(input);
        }
    }
}