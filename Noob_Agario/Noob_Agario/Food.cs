using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
namespace Noob_Agario
{
    internal class Food : CircleShape
    {
        int radius = 7;
        Random rnd = new Random();
        public Food(RenderWindow window)
        {
            Radius = radius;
            Position = new Vector2f(rnd.Next(0, (int)window.Size.X - radius * 2), rnd.Next(0, (int)window.Size.Y - radius * 2));
            byte r = (byte)rnd.Next(0, 255);
            byte g = (byte)rnd.Next(0, 255);
            byte b = (byte)rnd.Next(0, 255);
            FillColor = new Color(r, g, b);
        }
    }
}