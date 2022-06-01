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

        private Random rnd = new Random();
        public Player CreatePlayer(RenderWindow window, string name, bool isBot)
        {
            Player player = new Player(window, rnd, name, isBot);
            return player;
        }

        public Food CreateFood(RenderWindow window)
        {
            Food food = new Food(window, rnd);
            return food;
        }

        public Text CreateText(RenderWindow window, string text, uint size)
        {
            Font font = new Font("font/arial.ttf");
            Text newText = new Text(text, font, size);
            return newText;
        }
    }
}