using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Noob_Agario
{
    public class Controller
    {
        public Player currentOwner;

        private bool isBot;
        private Vector2f positionToGo = new Vector2f(0, 0);
        public Controller(bool isABot, Player player)
        {
            isBot = isABot;
            currentOwner = player;
        }
        public Vector2f GetInput(Player[] players, Vector2f position, float radius)
        {
            if (!isBot)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.W)) return new Vector2f(0, -1);
                if (Keyboard.IsKeyPressed(Keyboard.Key.S)) return new Vector2f(0, 1);
                if (Keyboard.IsKeyPressed(Keyboard.Key.A)) return new Vector2f(-1, 0);
                if (Keyboard.IsKeyPressed(Keyboard.Key.D)) return new Vector2f(1, 0);
                if (Keyboard.IsKeyPressed(Keyboard.Key.R)) ChangePlacesWithBot(players);
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
                if (positionToGo == new Vector2f(0, 0) || positionToGo == position)
                {
                    positionToGo = ObjectCreator.getInstance().GeneratePosition(radius);
                }
                Vector2f path = positionToGo - position;
                Vector2f normalizedPath = VectorManager.getInstance().normalize(path);
                return normalizedPath;
            }
            return new Vector2f(0,0);
        }

        private void ChangePlacesWithBot(Player[] players)
        {
            Player closestBot = null;
            float closestLength = float.MaxValue;
            foreach (Player player in players)
            {
                float length = VectorManager.getInstance().CalculateLength(player, currentOwner.Position);
                if (length < closestLength && player != currentOwner && player.isAlive)
                {
                    closestBot = player;
                    closestLength = length;
                }
            }
            (closestBot.controller, currentOwner.controller) = (currentOwner.controller, closestBot.controller);
        }
    }
}