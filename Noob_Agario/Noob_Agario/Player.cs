using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
namespace Noob_Agario
{
    internal class Player : CircleShape
    {
        public string name;
        public bool isBot;
        private int starterRadius = 40;
        public Player(RenderWindow window, Random rnd, string playerName)
        {
            name = playerName;
            Radius = starterRadius;
            Position = new Vector2f(rnd.Next(0, (int)window.Size.X - starterRadius * 2), rnd.Next(0, (int)window.Size.Y - starterRadius * 2));
            byte r = (byte)rnd.Next(1, 255);
            byte g = (byte)rnd.Next(1, 255);
            byte b = (byte)rnd.Next(1, 255);
            FillColor = new Color(r, g, b);


        }
    }
}