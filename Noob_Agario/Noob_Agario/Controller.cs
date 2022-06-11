using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Noob_Agario
{
    public class Controller
    {
        public Player currentOwner;

        public Controller(Player owner)
        {
            currentOwner = owner;
        }

        private Vector2f positionToGo = new Vector2f(0, 0);
        public (Vector2f, bool) GetInput(Vector2f position, float radius)
        {
            if (!currentOwner.isBot)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.W)) return (new Vector2f(0, -1), false);
                if (Keyboard.IsKeyPressed(Keyboard.Key.S)) return (new Vector2f(0, 1), false);
                if (Keyboard.IsKeyPressed(Keyboard.Key.A)) return (new Vector2f(-1, 0), false);
                if (Keyboard.IsKeyPressed(Keyboard.Key.D)) return (new Vector2f(1, 0), false);
                if (Keyboard.IsKeyPressed(Keyboard.Key.R)) return (new Vector2f(0, 0), true);
                /*if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    PositionToGo = (Vector2f)Mouse.GetPosition();
                    PositionToGo.X -= Radius;
                    PositionToGo.Y -= Radius;
                }
                Vector2f path = PositionToGo - Position;
                Vector2f normalizedPath = normalize(path);
                CheckIfCanMove(normalizedPath); */
            }
            else
            {
                if (positionToGo == new Vector2f(0, 0) || (positionToGo.X - position.X <= 5 && positionToGo.Y - position.Y <= 5))
                {
                    positionToGo = ObjectCreator.GeneratePosition(radius);
                }
                Vector2f path = positionToGo - position;
                Vector2f normalizedPath = path.Normalize();
                return (normalizedPath, false);
            }
            return (new Vector2f(0,0), false);
        }
    }
}