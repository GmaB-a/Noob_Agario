using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Noob_Agario
{
    public static class Extensions
    {
        public static Vector2f Normalize(this Vector2f vector)
        {
            float length = (float)Math.Sqrt((vector.X * vector.X) + (vector.Y * vector.Y));
            if (length != 0)
                return new Vector2f(vector.X / length, vector.Y / length);
            return vector;
        }

        public static float CalculateDistance(this float length, Vector2f player1Position, Vector2f player2Position)
        {
            float x = player1Position.X - player2Position.X;
            if (x < 0) x *= -1;
            float y = player1Position.Y - player2Position.Y;
            if (y < 0) x *= -1;
            float distance = (float)Math.Sqrt(x * x + y * y);
            return distance;
        }
    }
}