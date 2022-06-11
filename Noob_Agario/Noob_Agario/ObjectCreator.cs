using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Noob_Agario
{
    internal static class ObjectCreator
    {
        static RenderWindow _window;
        public static void GetWindow(RenderWindow window)
        {
            _window = window;
        }

        private static Random rnd = new Random();
        public static Player CreatePlayer(string name, bool isBot)
        {
            Player player = new Player(_window, name, isBot);
            return player;
        }

        public static CircleShape CreateCircle(float radius)
        {
            CircleShape circle = new CircleShape();
            circle.Radius = radius;
            circle.FillColor = GenerateColor();
            circle.Position = GeneratePosition(radius);
            return circle;
        }

        public static Food CreateFood()
        {
            Food food = new Food(_window);
            return food;
        }

        public static Text CreateText(string text, uint size)
        {
            Font font = new Font("font/arial.ttf");
            Text newText = new Text(text, font, size);
            return newText;
        }

        public static Vector2f GeneratePosition(float radius)
        {
            float x = GenerateRandomNumber(0, _window.Size.X - radius * 2);
            float y = GenerateRandomNumber(0, _window.Size.Y - radius * 2);
            return new Vector2f(x,y);
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

        public static Controller GenerateController(Player player)
        {
            Controller controller = new Controller(player);
            return controller;
        }
    }
}