using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Kata
{
    public static class Program
    {
        public static void Main()
        {
            var img = new Bitmap(Image.FromFile(@"C:\Users\inter\Downloads\Kata_Squares\2.jpg"));
            var resultImg = new Bitmap(img.Width, img.Height);

            var rectangles = img.ExtractRectangles();

            foreach (var rec in rectangles)
            {
                Console.WriteLine(rec);
            }

            using (Graphics g = Graphics.FromImage(resultImg))
            {
                // Ajouter un Background Blanc à l'image
                g.Clear(Color.White);

                // Dessiner les rectangles sur l'image
                foreach (Rectangle rectangle in rectangles)
                {
                    g.DrawRectangle(Pens.Black, rectangle);
                }

                // Enregistrer l'image avec les rectangles dessinés
                resultImg.Save(@"C:\Users\inter\Downloads\Kata_Squares\image_with_rectangles_result.png", ImageFormat.Png);
            }
        }
    }
}