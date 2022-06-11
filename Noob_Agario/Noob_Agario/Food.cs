using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
namespace Noob_Agario
{
    public  class Food : CircleShape
    {
        int radius = 7;
        Random rnd = new Random();
        public Food(RenderWindow window)
        {
            Radius = radius;
            Position = ObjectCreator.GeneratePosition(Radius);
            FillColor = ObjectCreator.GenerateColor();
        }
    }
}