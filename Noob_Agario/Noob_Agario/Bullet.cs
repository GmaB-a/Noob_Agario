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
    class Bullet
    {
        private Player owner;
        private Vector2f flyDirection;
        public CircleShape bulletModel;
        private float speed = 15f;
        public Bullet(Player creator, Vector2f direction)
        {
            owner = creator;
            flyDirection = direction;
            bulletModel = ObjectCreator.CreateCircle(7);
            bulletModel.Position = owner.position + new Vector2f(owner.radius, owner.radius);
        }
        public void Update(List<Player> players)
        {
            bulletModel.Position += flyDirection * speed;
            foreach(Player player in players)
            {
                if(bulletModel.Intersects(player.playerModel) && player != owner)
                {
                    player.Lose(3f);
                    Destroy();
                    break;
                }
            }
        }

        private void Destroy()
        {
            bulletModel.Radius = 0;
            Game.instance.bullets.Remove(this);
            Game.instance.shapesToDraw.Remove(bulletModel);
        }
    }
}