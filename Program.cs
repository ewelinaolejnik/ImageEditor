using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using ImageEditor;
using ImageEditor.Models;

namespace ImageEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            var iconInput = new IconInput()
            {
                Icon = new Icon("Darth-Vader.ico"),
                XCoordinate = 1300,
                YCoordinate = 600
            };
            var ellipseCropInput = new EllipseCropInput()
            {
                HeightOffset = 10,
                WidthOffset = 10
            };

            var rectangleCropInput = new RectangleCropInput()
            {
                DecreasePercentage = 25
            };

            EllipseImageEditor ellipseImageEditor = new EllipseImageEditor("ewelina.jpg");
            ellipseImageEditor.CropImage(ellipseCropInput);
            ellipseImageEditor.AddIcon(iconInput);
            ellipseImageEditor.SaveTargetImage("ewelina-ellipse.png");

            RectangleImageEditor rectangleImageEditor = new RectangleImageEditor("ewelina.jpg");
            rectangleImageEditor.CropImage(rectangleCropInput);
            rectangleImageEditor.AddIcon(iconInput);
            rectangleImageEditor.SaveTargetImage("ewelina-rectangle.png");
        }
    }
}