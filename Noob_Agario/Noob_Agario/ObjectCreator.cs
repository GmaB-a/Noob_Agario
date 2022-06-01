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
        public Player CreatePlayer(RenderWindow window, string name)
        {
            Player player = new Player(window, rnd, name);
            return player;
        }

        public Food CreateFood(RenderWindow window)
        {
            Food food = new Food(window, rnd);
            return food;
        }

        public Text CreateText(RenderWindow window, string text)
        {
            Text newText = new Text();
            newText.DisplayedString = text;
            return newText;
        }
    }
}