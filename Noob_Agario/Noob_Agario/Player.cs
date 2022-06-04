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
            Position = ObjectCreator.getInstance().GeneratePosition(Radius);
            FillColor = ObjectCreator.getInstance().GenerateColor();
            isBot = isABot;

            name = ObjectCreator.getInstance().CreateText(playerName, (uint)(Radius * 0.5f));
            name.Position = new Vector2f(Position.X + Radius * 0.7f, Position.Y + Radius * 0.7f);
        }

        private Vector2f PositionToGo = new Vector2f(0,0);
        Random rnd = new Random();
        public void GetInput(Random rnd)
        {
            if (!isBot)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.W)) CheckIfCanMove(new Vector2f(0, -1));
                if (Keyboard.IsKeyPressed(Keyboard.Key.S)) CheckIfCanMove(new Vector2f(0, 1));
                if (Keyboard.IsKeyPressed(Keyboard.Key.A)) CheckIfCanMove(new Vector2f(-1, 0));
                if (Keyboard.IsKeyPressed(Keyboard.Key.D)) CheckIfCanMove(new Vector2f(1, 0));
            }
            else
            {
                //CheckIfCanMove(rnd.Next(-1, 2), rnd.Next(-1, 2));
                if (PositionToGo == new Vector2f(0, 0) || PositionToGo == Position)
                {
                    PositionToGo = ObjectCreator.getInstance().GeneratePosition(Radius);
                }
                Vector2f path = PositionToGo - Position;
                Vector2f normalizedPath = normalize(path);
                Move(normalizedPath);
            }
        }

        private Vector2f normalize(Vector2f vector)
        {
            float length = (float)Math.Sqrt((vector.X * vector.X) + (vector.Y * vector.Y));
            if (length != 0)
                return new Vector2f(vector.X / length, vector.Y / length);
            return vector;
        }

        private void CheckIfCanMove(Vector2f path)
        {
            if ((Position.X - speed < 0) && path.X < 0) return;
            if ((Position.X + Radius * 2 + speed > _window.Size.X) && path.X > 0) return;

            if ((Position.Y - speed < 0) && path.Y < 0) return;
            if ((Position.Y + Radius * 2 + speed > _window.Size.Y) && path.Y > 0) return;
            Move(path);
        }

        private void Move(Vector2f path)
        {
            Vector2f newPosition = new Vector2f(Position.X + path.X * speed, Position.Y + path.Y * speed);
            Position = newPosition;
            name.Position = new Vector2f(Position.X + Radius * 0.7f, Position.Y + Radius * 0.7f);
        }

        public int TryEatPlayer(Player[] players, int currentPlayerCount)
        {
            foreach (Player player in players)
            {
                if(player != this && this.GetGlobalBounds().Intersects(player.GetGlobalBounds()) && Radius > player.Radius)
                {
                    Radius += player.Radius / 2;
                    player.Radius = 0;
                    player.name.DisplayedString = "";
                    currentPlayerCount--;
                }
            }
            return currentPlayerCount;
        }

        public void TryEatFood(Food[] foods, RenderWindow window, Random rnd)
        {
            foreach (Food food in foods)
            {
                if (this.GetGlobalBounds().Intersects(food.GetGlobalBounds()))
                {
                    Radius += 2;
                    food.Position = ObjectCreator.getInstance().GeneratePosition(food.Radius);
                }
            }
        }
    }
}