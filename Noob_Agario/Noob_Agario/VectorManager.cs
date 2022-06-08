using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
namespace Noob_Agario
{
    internal class VectorManager
    {
        private static VectorManager instance;
        public static VectorManager getInstance()
        {
            if (instance == null)
                instance = new VectorManager();
            return instance;
        }
        public Vector2f normalize(Vector2f vector)
        {
            float length = (float)Math.Sqrt((vector.X * vector.X) + (vector.Y * vector.Y));
            if (length != 0)
                return new Vector2f(vector.X / length, vector.Y / length);
            return vector;
        }

        public float CalculateLength(Player player, Vector2f Position)
        {
            float playerPosition = (float)Math.Sqrt((player.Position.X * player.Position.X) + (player.Position.Y * player.Position.Y));
            float thisLength = (float)Math.Sqrt((Position.X * Position.X) + (Position.Y * Position.Y));
            float distance = thisLength - playerPosition;
            if (distance < 0) distance *= -1;
            return distance;
        }
    }
}