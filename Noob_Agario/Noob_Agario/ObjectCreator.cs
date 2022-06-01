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

        public Player CreatePlayer(RenderWindow window)
        {
            Player player = new Player(window);
            return player;
        }

        public Food CreateFood(RenderWindow window)
        {
            Food food = new Food(window);
            return food;
        }
    }
}