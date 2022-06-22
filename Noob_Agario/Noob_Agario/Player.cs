using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void InputLogic(List<Player> players)
        {
            Vector2f moveDirection = controller.GetMovementDirection();
            if (moveDirection != new Vector2f(0, 0)) CheckIfCanMove(moveDirection);

            if (controller.WantsToChangeControllers()) ChangeControllersWithBot(players);

            if (controller.WantsToShoot()) Shoot(lastMovedDirection);
        }

        private void Shoot(Vector2f moveDirection)
        {
            Bullet newBullet = ObjectCreator.CreateBullet(this, moveDirection);
            Game.instance.OnPlayerShoot(newBullet);
            Lose(1f);
        }

        public void TryEatPlayer(List<Player> players)
        {
            foreach (Player player in players)
            {
                if (player != this && playerModel.Intersects(player.playerModel) && this.BiggerThan(player))
                {
                    Gain(player.radius / 2);
                    player.Die();
                }
            }
        }

        public void TryEatFood(List<Food> foods)
        {
            foreach (Food food in foods)
            {
                if (playerModel.Intersects(food.foodModel))
                {
                    Gain(2f);
                    food.Relocate();
                }
            }
        }

        public void Update(List<Player> players, List<Food> foods)
        {
            if (!isAlive) return;
            InputLogic(players);
            TryEatPlayer(players);
            TryEatFood(foods);
        }

        private void CheckIfCanMove(Vector2f direction)
        {
            if ((position.X - speed < 0) && direction.X < 0) direction.X = 0;
            if ((position.X + radius * 2 + speed > _window.Size.X) && direction.X > 0) direction.X = 0;

            if ((position.Y - speed < 0) && direction.Y < 0) direction.Y = 0;
            if ((position.Y + radius * 2 + speed > _window.Size.Y) && direction.Y > 0) direction.Y = 0;
            Move(direction);
        }

        public Vector2f lastMovedDirection = new Vector2f(1,0);

        private void Move(Vector2f direction)
        {
            position += direction * speed;
            lastMovedDirection = direction;
            name.Position = position + new Vector2f(radius * 0.7f, radius * 0.7f);
        }

        public void Gain(float amount)
        {
            radius += amount;
            if (radius > 350) radius = 350;
        }

        public void Lose(float amount)
        {
            radius -= amount;
            if (radius <= 0) Die();
        }

        private void Die()
        {
            radius = 0;
            name.DisplayedString = "";
            isAlive = false;
            Game.instance.OnPlayerEaten();
        }

        private void ChangeControllersWithBot(List<Player> players)
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