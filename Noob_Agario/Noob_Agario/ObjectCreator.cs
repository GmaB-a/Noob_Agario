using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static Bullet CreateBullet(Player creator, Vector2f moveDirection)
        {
            Bullet bullet = new Bullet(creator, moveDirection);
            return bullet;
        }

        public static CircleShape CreateCircle(float radius)
        {
            CircleShape circle = new CircleShape();
            circle.Radius = radius;
            circle.FillColor = RandomGenerator.GenerateColor();
            circle.Position = RandomGenerator.GeneratePosition(radius);
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
        public static Controller CreateController(Player player)
        {
            Controller controller = new Controller(player);
            return controller;
        }
    }
}