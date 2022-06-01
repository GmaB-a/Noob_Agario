using SFML.Window;
using SFML.Graphics;
using SFML.System;
namespace Noob_Agario
{
    internal class Food
    {
        CircleShape foodCircle;
        float radius = 7;
        public Food()
        {
            foodCircle = new CircleShape();
        }
    }
}