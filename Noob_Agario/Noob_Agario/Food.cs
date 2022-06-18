using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
namespace Noob_Agario
{
    public  class Food
    {
        Random rnd = new Random();

        public CircleShape foodModel;
        public Vector2f Position()
            => foodModel.Position;

        public float Radius()
            => foodModel.Radius;

        public void Relocate()
        {
            foodModel.Position = RandomGenerator.GeneratePosition(Radius());
        }

        public Food(RenderWindow window)
        {
            foodModel = ObjectCreator.CreateCircle(7);
        }
    }
}