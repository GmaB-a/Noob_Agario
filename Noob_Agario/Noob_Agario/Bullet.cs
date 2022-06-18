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
        private CircleShape bulletModel;
        private float speed = 15f;
        public Bullet(Player creator, Vector2f direction)
        {
            owner = creator;
            flyDirection = direction;
            bulletModel = ObjectCreator.CreateCircle(3);
            bulletModel.Position = owner.position;
        }
        public void Update(List<Player> players)
        {
            bulletModel.Position += flyDirection * speed;
            foreach(Player player in players)
            {
                if(bulletModel.Intersects(player.playerModel) && player != owner)
                {
                    player.radius -= 10f;
                }
            }
        }

    }
}