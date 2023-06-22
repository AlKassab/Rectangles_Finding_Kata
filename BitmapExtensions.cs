using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection.Metadata.Ecma335;

namespace Kata
{
    internal static class BitmapExtensions
    {
        private static double CompareTo(this Color e1, Color e2)
        {
            long rmean = (e1.R + e2.R) / 2;
            long r = e1.R - e2.R;
            long g = e1.G - e2.G;
            long b = e1.B - e2.B;
            var diff = Math.Sqrt((((512 + rmean) * r * r) >> 8) + 4 * g * g + (((767 - rmean) * b * b) >> 8));

            return diff;
        }
        private static bool IsContrast(this Color pixel) => pixel.CompareTo(Color.White) > 50;

        public static Rectangle[] ExtractRectangles(this Bitmap img)
        {
            List<Rectangle> rectangles = new List<Rectangle>();

            bool[,] visited = new bool[img.Width, img.Height];

            // Parcours de l'image pixel par pixel
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    if (!visited[x, y])
                    {
                        Color pixel = img.GetPixel(x, y);

                        // Vérifier si le pixel est contrasté
                        if (pixel.IsContrast())
                        {
                            Rectangle rectangle = FindRectangle(img, x, y, visited);
                            rectangles.Add(rectangle);
                        }
                    }
                }
            }

            return rectangles.ToArray();
        }

        private static Rectangle FindRectangle(Bitmap img, int startX, int startY, bool[,] visited)
        {
            int endX = startX;
            int endY = startY;

            // Trouver la limite la plus à droite du rectangle
            while (endX < img.Width && !visited[endX, startY] && img.GetPixel(endX, startY).IsContrast())
            {
                visited[endX, startY] = true;
                endX++;
            }

            // Trouver la limite la plus à droite du rectangle
            for (int x = startX; x < endX; x++)
            {
                for (int y = startY + 1; y < img.Height; y++)
                {
                    if (!visited[x, y] && img.GetPixel(x, y).IsContrast())
                    {
                        visited[x, y] = true;
                        endY = Math.Max(endY, y);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return new Rectangle(startX, startY, endX - startX, endY - startY);
        }

    }
}

