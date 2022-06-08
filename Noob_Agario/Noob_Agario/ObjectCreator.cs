using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Noob_Agario
{
    internal class ObjectCreator
    {
        private static ObjectCreator instance;
        public static ObjectCreator getInstance()
        {
            if (instance == null)
                instance = new ObjectCreator();
            return instance;
        }

        RenderWindow _window;
        public void GetWindow(RenderWindow window)
        {
            _window = window;
        }

        private Random rnd = new Random();
        public Player CreatePlayer(string name, bool isBot)
        {
            Player player = new Player(_window, name, isBot);
            return player;
        }

        public Food CreateFood()
        {
            Food food = new Food(_window);
            return food;
        }

        public Text CreateText(string text, uint size)
        {
            Font font = new Font("font/arial.ttf");
            Text newText = new Text(text, font, size);
            return newText;
        }

        public Vector2f GeneratePosition(float radius)
        {
            float x = GenerateRandomNumber(0, _window.Size.X - radius * 2);
            float y = GenerateRandomNumber(0, _window.Size.Y - radius * 2);
            return new Vector2f(x,y);
        }

        public float GenerateRandomNumber(float min, float max)
            => rnd.Next((int)min, (int)max);

        public Color GenerateColor()
        {
            byte r = (byte)rnd.Next(1, 255);
            byte g = (byte)rnd.Next(1, 255);
            byte b = (byte)rnd.Next(1, 255);
            return new Color(r, g, b);
        }

        public Controller GenerateController(bool isBot, Player player)
        {
            Controller controller = new Controller(isBot, player);
            return controller;
        }
    }
}