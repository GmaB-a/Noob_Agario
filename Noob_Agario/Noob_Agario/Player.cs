using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
namespace Noob_Agario
{
    internal class Player : CircleShape
    {
        private RenderWindow _window;

        public Text name;
        public bool isBot;

        private int starterRadius = 30;
        private float speed = 4f;

        public Player(RenderWindow window, Random rnd, string playerName, bool isABot)
        {
            _window = window;

            Radius = starterRadius;
            Position = new Vector2f(rnd.Next(0, (int)window.Size.X - starterRadius * 2), rnd.Next(0, (int)window.Size.Y - starterRadius * 2));
            byte r = (byte)rnd.Next(1, 255);
            byte g = (byte)rnd.Next(1, 255);
            byte b = (byte)rnd.Next(1, 255);
            FillColor = new Color(r, g, b);
            isBot = isABot;

            name = ObjectCreator.getInstance().CreateText(window, playerName, (uint)(Radius * 0.5f));
            name.Position = new Vector2f(Position.X + Radius * 0.7f, Position.Y + Radius * 0.7f);
        }

        public void GetInput(Random rnd)
        {
            if (!isBot)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.W)) CheckIfCanMove(0, -1);
                if (Keyboard.IsKeyPressed(Keyboard.Key.S)) CheckIfCanMove(0, 1);
                if (Keyboard.IsKeyPressed(Keyboard.Key.A)) CheckIfCanMove(-1, 0);
                if (Keyboard.IsKeyPressed(Keyboard.Key.D)) CheckIfCanMove(1, 0);
            }
            else
            {
                CheckIfCanMove(rnd.Next(-1, 2), rnd.Next(-1, 2));
            }
        }

        private void CheckIfCanMove(int dx, int dy)
        {
            if ((Position.X - speed < 0) && dx < 0) return;
            if ((Position.X + Radius * 2 + speed > _window.Size.X) && dx > 0) return;

            if ((Position.Y - speed < 0) && dy < 0) return;
            if ((Position.Y + Radius * 2 + speed > _window.Size.Y) && dy > 0) return;
            Move(dx, dy);
        }

        private void Move(int dx, int dy)
        {
            Vector2f newPosition = new Vector2f(Position.X + dx * speed, Position.Y + dy * speed);
            Position = newPosition;
            name.Position = new Vector2f(Position.X + Radius * 0.7f, Position.Y + Radius * 0.7f);
        }

        public int TryEatPlayer(Player[] players, int currentPlayerCount)
        {
            for(int i = 0; i < players.Length; i++)
            {
                if(players[i] != this && this.GetGlobalBounds().Intersects(players[i].GetGlobalBounds()) && Radius > players[i].Radius)
                {
                    Radius += players[i].Radius / 2;
                    players[i].Radius = 0;
                    players[i].name.DisplayedString = "";
                    currentPlayerCount--;
                }
            }
            return currentPlayerCount;
        }

        public void TryEatFood(Food[] foods, RenderWindow window, Random rnd)
        {
            for (int i = 0; i < foods.Length; i++)
            {
                if (this.GetGlobalBounds().Intersects(foods[i].GetGlobalBounds()))
                {
                    Radius += 2;
                    foods[i].Position = foods[i].RandomizePosition(window, rnd);
                }
            }
        }
    }
}