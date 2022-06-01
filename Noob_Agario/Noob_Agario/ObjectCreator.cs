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

        public Player CreatePlayer()
        {
            Player player = new Player();
            return player;
        }

        public Food CreateFood()
        {
            Food food = new Food();
            return food;
        }
    }
}