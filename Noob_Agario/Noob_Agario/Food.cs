using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
namespace Noob_Agario
{
    internal class Food : CircleShape
    {
        int radius = 7;
        public Food(RenderWindow window, Random rnd)
        {
            Radius = radius;
            Position = ObjectCreator.getInstance().GeneratePosition(Radius);
            FillColor = ObjectCreator.getInstance().GenerateColor();
        }
    }
}