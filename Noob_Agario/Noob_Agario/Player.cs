using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
namespace Noob_Agario
{
    public class Player : CircleShape
    {
        private RenderWindow _window;

        public Text name;
        public bool isAlive;

        private int starterRadius = 30;
        private float speed = 4f;

        public Controller controller;

        public Player(RenderWindow window, string playerName, bool isABot)
        {
            _window = window;

            Radius = starterRadius;
            Position = ObjectCreator.getInstance().GeneratePosition(Radius);
            FillColor = ObjectCreator.getInstance().GenerateColor();
            isAlive = true;

            controller = ObjectCreator.getInstance().GenerateController(isABot, this);

            name = ObjectCreator.getInstance().CreateText(playerName, (uint)(Radius * 0.5f));
            name.Position = new Vector2f(Position.X + Radius * 0.7f, Position.Y + Radius * 0.7f);
        }

        public void MoveLogic(Player[] players)
        {
            CheckIfCanMove(controller.GetInput(players, Position, Radius));
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
                    player.isAlive = false;
                }
            }
            return currentPlayerCount;
        }

        public void TryEatFood(Food[] foods)
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