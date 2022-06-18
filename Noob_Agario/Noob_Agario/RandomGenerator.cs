using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
namespace Noob_Agario
{
    static class RandomGenerator
    {
        static RenderWindow _window;
        public static void GetWindow(RenderWindow window)
        {
            _window = window;
        }

        private static Random rnd = new Random();
        public static Vector2f GeneratePosition(float radius)
        {
            float x = GenerateRandomNumber(0, _window.Size.X - radius * 2);
            float y = GenerateRandomNumber(0, _window.Size.Y - radius * 2);
            return new Vector2f(x, y);
        }

        public static float GenerateRandomNumber(float min, float max)
            => rnd.Next((int)min, (int)max);

        public static Color GenerateColor()
        {
            byte r = (byte)rnd.Next(1, 255);
            byte g = (byte)rnd.Next(1, 255);
            byte b = (byte)rnd.Next(1, 255);
            return new Color(r, g, b);
        }
    }
}