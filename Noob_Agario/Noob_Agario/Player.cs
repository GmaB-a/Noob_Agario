using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
namespace Noob_Agario
{
    public class Player
    {
        private RenderWindow _window;

        public CircleShape playerModel;

        public Vector2f position
        {
            get => playerModel.Position;
            set => playerModel.Position = value;
        }

        public float radius
        {
            get => playerModel.Radius;
            set => playerModel.Radius = value;
        }

        public Text name;
        public bool isAlive;
        public bool isBot;

        private int starterRadius = 30;
        private float speed = 4f;

        public Controller controller;

        public Player(RenderWindow window, string playerName, bool isABot)
        {
            _window = window;

            playerModel = ObjectCreator.CreateCircle(starterRadius);
            isAlive = true;
            isBot = isABot;

            controller = ObjectCreator.CreateController(this);

            name = ObjectCreator.CreateText(playerName, (uint)(playerModel.Radius * 0.5f));
            name.Position = new Vector2f(position.X + radius * 0.7f, position.Y + radius * 0.7f);
        }

        public void InputLogic(Player[] players)
        {
            Vector2f movePosition = controller.GetMovementDirection();
            CheckIfCanMove(movePosition);

            if (controller.WantsToChangeControllers()) ChangeControllersWithBot(players);
        }

        public void TryEatPlayer(Player[] players)
        {
            foreach (Player player in players)
            {
                if (player != this && playerModel.Intersects(player.playerModel) && this.BiggerThan(player))
                {
                    radius += player.radius / 2;
                    player.Die();
                }
            }
        }

        public void TryEatFood(Food[] foods)
        {
            foreach (Food food in foods)
            {
                if (playerModel.Intersects(food.foodModel))
                {
                    radius += 2;
                    food.Relocate();
                }
            }
        }

        public void Update(Player[] players, Food[] foods)
        {
            InputLogic(players);
            TryEatPlayer(players);
            TryEatFood(foods);
        }
        private void CheckIfCanMove(Vector2f direction)
        {
            if ((position.X - speed < 0) && direction.X < 0) return;
            if ((position.X + radius * 2 + speed > _window.Size.X) && direction.X > 0) return;

            if ((position.Y - speed < 0) && direction.Y < 0) return;
            if ((position.Y + radius * 2 + speed > _window.Size.Y) && direction.Y > 0) return;
            Move(direction);
        }

        private void Move(Vector2f direction)
        {
            position += direction * speed;
            name.Position = position + new Vector2f(radius * 0.7f, radius * 0.7f);
        }

        private void Die()
        {
            radius = 0;
            name.DisplayedString = "";
            isAlive = false;
            Game.instance.OnPlayerEaten();
        }

        private void ChangeControllersWithBot(Player[] players)
        {
            Player closestBot = null;
            float closestLength = float.MaxValue;
            foreach (Player player in players)
            {
                float length = 0;
                length.CalculateDistance(player.playerModel.Position, playerModel.Position);
                if (length < closestLength && player != this && player.isAlive)
                {
                    closestBot = player;
                    closestLength = length;
                }
            }
            if (closestBot != null)
            {
                (closestBot.controller, controller) = (controller, closestBot.controller);
            }
        }
    }
}