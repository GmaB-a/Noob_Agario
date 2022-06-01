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
    internal class Game
    {
        public RenderWindow window;

        private static int maxPlayers = 10;
        private static int maxFood = 25;

        private Shape[] players = new Shape[maxPlayers];
        private Shape[] foods = new Shape[maxFood];
        private Shape[] toDraw = new Shape[maxPlayers + maxFood];
        public void Play()
        {
            window = new RenderWindow(new VideoMode(1600, 900), "Game window");
            window.SetFramerateLimit(60);
            window.Closed += WindowClosed;

            CreatePlayers();
            CreateFood();
            
            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear();

                for(int i = 0; i < toDraw.Length; i++)
                {
                    window.Draw(toDraw[i]);
                }
                window.Display();
            }
        }

        private void CreatePlayers()
        {
            players[0] = ObjectCreator.getInstance().CreatePlayer(window, "you");
            for (int i = 1; i < maxPlayers; i++)
            {
                players[i] = ObjectCreator.getInstance().CreatePlayer(window, "bot");
            }
            players.CopyTo(toDraw, 0);
        }

        private void CreateFood()
        {
            for (int i = 0; i < maxFood; i++)
            {
                foods[i] = ObjectCreator.getInstance().CreateFood(window);
            }
            foods.CopyTo(toDraw, players.Length);
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }
    }
}