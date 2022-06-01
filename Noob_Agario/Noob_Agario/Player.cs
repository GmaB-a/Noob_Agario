using SFML.Window;
using SFML.Graphics;
using SFML.System;
namespace Noob_Agario
{
    internal class Player
    {
        public CircleShape playerCircle;
        private float starterRadius = 10;
        private float currentRadius;
        public Player()
        {
            playerCircle = new CircleShape();
        }
    }
}